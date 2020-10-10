using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class InvalidEventDomainException : Exception
    {
        public InvalidEventDomainException(string message)
            : base(message)
        { }
    }
}
