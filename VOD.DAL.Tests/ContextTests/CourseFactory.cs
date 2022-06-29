using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.Entities;

namespace VOD.Database.Tests.ContextTests
{
    public class CourseFactory 
    {
        public static Course NewFirstCourse()
        {
            return new Course
            {
                Title = "Essential EF Core",
                Description = "Dive into the hidden depths of the Framework",
                Instructor = new Instructor { Name = "Troelsen Japikse" }
            };
        }

        public static Course NewSecondCourse()
        {
            return new Course
            {
                Title = "Docker in Nutshell",
                Description = "Explore the revolutionary containerization tool",
                Instructor = new Instructor { Name = "Russ McKendrick" }
            };
        }
    }
}
