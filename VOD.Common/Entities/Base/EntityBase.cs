using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOD.Common.Entities.Base
{
    public abstract class EntityBase
    {
        [Timestamp]
        public byte[] TimeStamp { get; set; }
    }
}
