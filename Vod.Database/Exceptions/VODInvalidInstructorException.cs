using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOD.Database.Exceptions
{
    public class VODInvalidInstructorException : VideoOnDemandException
    {
        public VODInvalidInstructorException() {}
        public VODInvalidInstructorException(string message) : base(message) {}
        public VODInvalidInstructorException(string message, Exception innerException)  : base(message, innerException) { }
    }
}
