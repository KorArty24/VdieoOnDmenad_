using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOD.Database.Exceptions
{
    public class VODLimitExceededException : VideoOnDemandException
    {
        public VODLimitExceededException() {}
        public VODLimitExceededException(string message) : base(message) {}
        public VODLimitExceededException(string message, Exception innerException)  : base(message, innerException) { }
    }
}
