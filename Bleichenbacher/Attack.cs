using Bleichenbacher.Exceptions;
using Bleichenbacher.Key;
using System.Collections.Generic;
using System.Numerics;

namespace Bleichenbacher
{
    class Interval
    {
        public BigInteger Left;
        public BigInteger Right;
    }

    class Attack
    {
        public Attack(RSA_PKCS RSA, PublicKey publicKey, PrivateKey privateKey)
        {
            this.RSA = RSA;
            PublicKey = publicKey;
            PrivateKey = privateKey;
            B = BigInteger.Pow(2, 8 * (int.Parse(publicKey.getK().ToString()) - 2));
            Intervals = new List<Interval>();
        }



        private RSA_PKCS RSA;

        private PublicKey PublicKey;

        private PrivateKey PrivateKey;

        private BigInteger B;

        private BigInteger S = 1;

        private List<Interval> Intervals;

        private BigInteger rightS = 0;

        private BigInteger S0;



        public void Start(string cipherText)
        {
            BigInteger r;
            BigInteger previousS;
            bool finded;
            int i = 0;
            while (true)
            {
                switch (i)
                {
                    case 0:
                        CheckItems(cipherText);
                        Intervals.Add(new Interval()
                            {
                                Left = 2 * B,
                                Right = 3 * B - 1
                            }
                        );
                        S0 = S;
                        break;
                    case 1:
                        S = PublicKey.GetN() / (2 * B);
                        CheckItems(cipherText);
                        NarrowInterval();
                        if (CheckIntervals())
                        {
                            return;
                        }
                        break;
                    default:
                        if (Intervals.Count == 1)
                        {
                            finded = false;
                            previousS = S;
                            r = 2 * (Intervals[0].Right * previousS - 2 * B) / PublicKey.GetN();
                            while (!finded)
                            {
                                S = (2 * B + r * PublicKey.GetN()) / Intervals[0].Right;
                                rightS = (3 * B + r * PublicKey.GetN()) / Intervals[0].Left;
                                for (; S < rightS; S++)
                                {
                                    try
                                    {
                                        CheckItem(cipherText);
                                    }
                                    catch (DecryptionErrorExecption)
                                    {
                                        continue;
                                    }
                                    finded = true;
                                    break;
                                }
                                r++;
                            }
                        } else
                        {
                            S++;
                            CheckItems(cipherText);
                        }
                        NarrowInterval();
                        if (CheckIntervals())
                        {
                            return;
                        }
                        break;
                }
                i++;
            }
        }

        private void CheckItem(string cipherText)
        {
            BigInteger c = RSA.OS2IP(cipherText);
            BigInteger newC;
            newC = c * RSA.PowModul(S, PublicKey.getE(), PublicKey.GetN());
            RSA.Decode(RSA.I2OSP(newC, PrivateKey.getK()));
        }

        private void CheckItems(string cipherText)
        {
            BigInteger c = RSA.OS2IP(cipherText);
            BigInteger newC;
            while (true)
            {
                newC = c * RSA.PowModul(S, PublicKey.getE(), PublicKey.GetN()) % PublicKey.GetN();
                try
                {
                    RSA.Decode(RSA.I2OSP(newC, PrivateKey.getK()));
                } catch (DecryptionErrorExecption)
                {
                    S++;
                    continue;
                }
                break;
            }
            return;
        }

        private void NarrowInterval()
        {
            BigInteger r;
            BigInteger maxR;
            BigInteger newLeft;
            BigInteger newRight;
            BigInteger tmp; 
            List<Interval> newIntervals = new List<Interval>();
            Interval newInterval;
            foreach(Interval interval in Intervals)
            {
                r = (interval.Left * S - 3 * B + 1) / PublicKey.GetN();
                maxR = (interval.Right * S - 2 * B) / PublicKey.GetN(); 
                for (; r <= maxR; r++)
                {
                    newLeft = 2 * B + r * PublicKey.GetN();
                    newLeft = newLeft % S == 0 ? newLeft / S : newLeft / S + 1;
                    newRight = (3 * B - 1 + r * PublicKey.GetN()) / S;
                    newInterval = new Interval()
                    {
                        Left = interval.Left < newLeft ? newLeft : interval.Left,
                        Right = interval.Right > newRight ? newRight : interval.Right
                    };
                    if (newInterval.Left <= newInterval.Right)
                    {
                        newIntervals.Add(newInterval);
                    }
                }
            }
            Intervals = newIntervals;
        }

        private bool CheckIntervals()
        {
            foreach(Interval interval in Intervals)
            {
                if (interval.Left == interval.Right)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
