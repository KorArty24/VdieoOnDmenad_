using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.Entities.Base;

namespace VOD.Common.Entities
{
    public class Instructor: EntityBase
    {
        [Key]
        public int Id { get; private set; }

        [MaxLength(80), Required]
        public string Name { get; private set; }

        [MaxLength(1024)]
        public string Description { get; private set; }

        [MaxLength(1024)]
        public string Thumbnail { get; private set; }

        // public List<Course> Courses { get; private set; }
        private HashSet<Course> _courses;

        public IReadOnlyCollection<Course> Courses => _courses?.ToList();
    }
}
