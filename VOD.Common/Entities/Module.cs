using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.Entities.Base;

namespace VOD.Common.Entities
{
    public class Module: EntityBase
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(80), Required]
        public string Title { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }

        public List<Video> Videos { get; set; }

        public List<Download> Downloads { get; set; }
    }
}
