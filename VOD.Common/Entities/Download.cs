using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.Entities.Base;

namespace VOD.Common.Entities
{
    public class Download: EntityBase
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(80), Required]
        public string Title { get; set; }

        [MaxLength(1024)]
        public string Url { get; set; }

        public int ModuleId { get; set; }

        public int CourseId { get; set; }

        public Module Module { get; set; }

        public Course Course { get; set; }

    }
}
