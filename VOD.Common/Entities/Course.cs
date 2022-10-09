using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.Entities.Base;
using StatusGeneric;

namespace VOD.Common.Entities
{
    [Table ("Courses")]
    public class Course: EntityBase
    {
        [Key]
        public int Id { get; private set; }

        [DataType(DataType.Text),MaxLength(255)]
        public string ImageUrl { get; private set; }

        [DataType(DataType.Text), MaxLength(80), Required]
        public string Title { get; private set; }

        [DataType(DataType.Text), MaxLength(255)]
        public string MarqueeImageUrl { get; private set; }

        [DataType(DataType.Text), MaxLength(1024), Required]
        public string Description { get; private set; }

        public int InstructorId { get; private set; }
        
        public Instructor Instructor { get; private set; }

        // public List<Module> Modules { get; private set; }

        private HashSet<Module> _modules;

        public IReadOnlyCollection<Module> Modules => _modules?.ToList();

        private Course() { }

        public static IStatusGeneric<Course> CreateCourse(
            int id, string imageUrl, string title, string marqueeImageUrl,
            string description, int instructorId, Instructor instructor, ICollection<Module> modules)
        {
            var status = new StatusGenericHandler<Course>();
            if (string.IsNullOrWhiteSpace(title))
                status.AddError("The Course title cannot be empty!");
            var course = new Course
            {
                Id = id,
                Title = title,
                MarqueeImageUrl = marqueeImageUrl,
                Description = description,
                InstructorId = instructorId,
                Instructor = instructor,
            };
            if (instructorId == null)
                throw new ArgumentNullException(nameof(instructorId));
            byte order = 0;
            if (description == null)
                throw new ArgumentNullException(nameof(description));
            if (title == null)
                throw new ArgumentNullException(nameof(title));
            if (modules == null)
                throw new ArgumentNullException(nameof(modules));
            course._modules = new IList<Module>(modules.Select(m=> new Module()))
        };
            
    }
}
