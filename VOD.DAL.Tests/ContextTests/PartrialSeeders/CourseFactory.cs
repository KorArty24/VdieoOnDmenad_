using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.Entities;

namespace VOD.Database.Tests.ContextTests.PartrialSeeders
{
    public class CourseFactory
    {
        public static Course NewFirstCourse()
        {
            return new Course
            {
                Title = "Course 1. Essential EF Core",
                Description = "Dive into the hidden depths of the Framework",
                Instructor = new Instructor { Name = "John Doe" }
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

        public static IEnumerable<Module> ReturnNewModuleandVideoForCourse(int courseId)
        {
            List<Module> modules = new List<Module>
            {
                new Module
                {
                Title = "Module 1",
                CourseId = courseId,
                Videos = new List<Video>
                {
                    new Video
                    {
                        Title = "Warm up",
                        CourseId = courseId,
                        Duration = 20
                    },
                    new Video
                    {
                        Title = "Fun begins",
                        CourseId= courseId,
                        Duration = 10
                    }
                }
                },
                new Module
                {
                    Title = "Module 2",
                    CourseId= courseId,
                    Videos = new List<Video>
                    {
                        new Video{Title = "Video 3", CourseId = courseId, Duration = 15 } // total for course = 45
                    }
                }
            };
             return modules;
        }
       
    }
}
