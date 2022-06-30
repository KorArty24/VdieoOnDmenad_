using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOD.Database.Exceptions
{
    public class VideoOnDemandException : Exception
    {
        public VideoOnDemandException() { }
        public VideoOnDemandException(string message) : base(message) { }
        public VideoOnDemandException(string message, Exception innerException) : base(message, innerException)
        { }
            
    }
}
