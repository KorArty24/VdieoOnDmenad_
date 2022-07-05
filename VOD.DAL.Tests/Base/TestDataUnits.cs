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
                Title = "Course 1. Essential EF Core",
                Description = "Dive into the hidden depths of the Framework",
                Instructor = new Instructor { Name = "John Doe" },
                Modules = new List<Module>
                {
                 new Module
                 {
                     Title = "Module 1. Warm Up"
                 }
                //1st module
                }
            };
        }
    }
}
