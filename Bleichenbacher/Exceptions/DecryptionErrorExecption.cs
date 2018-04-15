using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bleichenbacher.Exceptions
{
    class DecryptionErrorExecption : Exception
    {
        public DecryptionErrorExecption(string message)
        {
            Message = message;
        }



        private new string Message;



        public string getMessage()
        {
            return Message;
        }
    }
}
