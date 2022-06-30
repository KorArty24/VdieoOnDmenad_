using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOD.Database.Exceptions
{
    public class VideoOnDemandConcurencyException : VideoOnDemandException
    {
        public VideoOnDemandConcurencyException() { }
        public VideoOnDemandConcurencyException(string message) : base(message) { }
        public VideoOnDemandConcurencyException(string message, Exception innerException) : base(message, innerException) { }
    }
}
