using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using VOD.Common.Entities;
using VOD.Database.Contexts;
using VOD.Database.Migrations.DbInitializer;

namespace VOD.DAL.Tests.ContextTests
{
    
    public class CourseTests : IDisposable
    {
        private readonly VODContext _db;

        public CourseTests()
        {
            _db = new VODContextFactory().CreateDbContext(new string[0]);
            
            CleanDatabase();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        private void CleanDatabase()
        {
            SampleDataInitializer.ClearData(_db);
        }

        [Test]
        public void FirstTest() 
        {
            Assert.True(true);
        }

        [Test]
        public void ShouldAddACourseWithDbSet()
        {
            //Arrange 

            var course = new Course { Title = "Essential EF", Description = "Start building your apps with EF",
                Instructor= new Instructor { Name = "Phil Japikse", } };

            //Act 

            _db.Courses.Add(course);

            // Assert
            Assert.That(_db.Entry(course).State, Is.EqualTo(EntityState.Added));
          //  Assert.True(course.Id > 0);
           // Assert.Null(course.TimeStamp);
            _db.SaveChanges();
            Assert.That(_db.Entry(course).State, Is.EqualTo(EntityState.Unchanged));
            Assert.NotNull(course.TimeStamp);
            Assert.That(_db.Courses.Count(), Is.EqualTo(1));
        }
    }
}
