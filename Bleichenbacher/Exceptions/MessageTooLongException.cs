using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bleichenbacher.Exceptions
{
    class MessageTooLongException : Exception
    {
        public string message = "Message too long";
    }
}
