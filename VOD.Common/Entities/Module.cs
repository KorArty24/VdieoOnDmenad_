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
        private Module() { }

        [Key]
        public int Id { get; set; }

        [MaxLength(80), Required]
        public string Title { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }

        //public List<Video> Videos { get; set; }
        private HashSet<Course> _courses = new HashSet<Course>();
        public IReadOnlyCollection<Course> Courses => _courses?.ToList();

        //public List<Download> Downloads { get; set; }
        private HashSet<Download> _downloads = new HashSet<Download>();
        public IReadOnlyCollection<Download> Downloads => _downloads?.ToList();
    }
}
