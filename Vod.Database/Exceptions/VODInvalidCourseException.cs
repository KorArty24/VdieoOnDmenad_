using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOD.Database.Exceptions
{
    public class VODInvalidCourseException : VideoOnDemandException
    {
        public VODInvalidCourseException() {}
        public VODInvalidCourseException(string message) : base(message) {}
        public VODInvalidCourseException(string message, Exception innerException)  : base(message, innerException) { }
    }
}
