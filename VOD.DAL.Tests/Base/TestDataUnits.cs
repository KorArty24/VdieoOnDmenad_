using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.Entities;

namespace VOD.Database.Tests.Base
{
    public static class TestDataUnits
    {
        public static Course NewCourseWithInstructorAndModule()
        {
            return new Course
            {
                Id = 10005,
                Title = "Course 1. Essential EF Core",
                Description = "Dive into the hidden depths of the Framework",
                Instructor = new Instructor {
                Name = "John Doe",
                Description = "#1 in computer literature sales on Amazon"},
                Modules = new List<Module>
                {
                     new Module
                     {
                         CourseId = 10005,
                         Title = "Module 1. Warm Up",
                         Videos = new List<Video>
                         {
                             new Video
                             {
                                CourseId = 10005,
                                Title = "Video1. Module1",
                                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit," ,
                             }
                         }
                     }
                //1st module
                }
            };
        }
    }
}
