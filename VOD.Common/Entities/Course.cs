using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.Entities.Base;

namespace VOD.Common.Entities
{
    [Table ("Courses")]
    public class Course: EntityBase
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Text),MaxLength(255)]
        public string ImageUrl { get; set; }

        [DataType(DataType.Text), MaxLength(80), Required]
        public string Title { get; set; }

        [DataType(DataType.Text), MaxLength(255)]
        public string MarqueeImageUrl { get; set; }

        [DataType(DataType.Text), MaxLength(1024), Required]
        public string Description { get; set; }

        public int InstructorId { get; set; }
        
        public Instructor Instructor { get; set; }

        public List<Module> Modules { get; set; }
    }
}
