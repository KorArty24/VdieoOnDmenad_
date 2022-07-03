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

namespace VOD.Database.Tests.ContextTests
{

    public class CourseListTests : CourseTestBase
    {
        private readonly VODContext _db;

        public CourseListTests()
        {
            _db= new VODContextFactory().CreateDbContext(new string[0]);
            CleanDatabase();
        }

        [Test]
        public void ShouldReturnInstructorForCourse()
        {
            //Arrange 
            SampleDataInitializer.InitializeData(_db);
            //Act 
            string courseName = "Course 1";
            //var course = _db.Courses.Where(x => x.Title.Contains(courseName)).SelectMany(y => y.Modules).SelectMany(p => p.Videos).ToList();
            //var dur = course.Select(x => x.Duration).Sum();
            //int dur = _db.Videos.Where(x => x.Course.Title.Contains(courseName)).Sum(y => y.Duration);
            string instructor = _db.Courses.Where(c => c.Title.Contains(courseName)).Select(x => x.Instructor.Name).First();
            //Assert 
            Assert.That(instructor, Is.EqualTo("John Doe"));
        }

        //[Test]
        //public voidShould

    }
}
