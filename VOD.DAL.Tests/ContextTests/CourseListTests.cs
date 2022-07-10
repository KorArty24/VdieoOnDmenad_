using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Database.Contexts;
using VOD.Database.Migrations.DbInitializer;
using VOD.Database.Tests.Base;
using VOD.Database.Tests.ContextTests.PartrialSeeders;

namespace VOD.Database.Tests.ContextTests
{

    public class CourseListTests : TestBase
    {
         protected VODContext _db
        {
            get { return this.context; }
        }

        [Test]
        public void ShouldReturnInstructorForCourse()
        {
            //Arrange 
            var course = CourseFactory.NewFirstCourse();
            context.Courses.Add(course);
            context.SaveChangesAsync();
            //Act 
            string courseName = "Course 1";
            string instructor = context.Courses.Where(c => c.Title.Contains(courseName)).Select(x => x.Instructor.Name).First();
            //Assert 
            Assert.That(instructor, Is.EqualTo("John Doe"));
        }

        //[Test]
        //public voidShould

    }
}
