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
        internal Module( Course course, string title, int courseId)
        {
            Course = course;
            Title = title;
            CourseId = courseId;
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(80), Required]
        public string Title { get; private set; }

        //Fully defined relationship
        public int CourseId { get; private set; }

        public Course Course { get; private set; }

        //public List<Video> Videos { get; set; }
        private ICollection<Video> _videos = new List<Video>();
        public IReadOnlyCollection<Video> Courses => _videos?.ToList();

        //public List<Download> Downloads { get; set; }
        private HashSet<Download> _downloads = new HashSet<Download>();
        public IReadOnlyCollection<Download> Downloads => _downloads?.ToList();

    }
}
