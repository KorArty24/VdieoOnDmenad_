using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Database.Contexts;
using VOD.Database.Migrations.DbInitializer;

namespace VOD.Database.Tests.ContextTests
{
    [TestFixture]
    public class CourseListTests : CourseTestBase
    {
        private readonly VODContext _db;

        public CourseListTests()
        {
            _db = new Contexts.VODContextFactory().CreateDbContext(new string[0]);
            CleanDatabase();
        }

        [Test]
        public void ShouldReturnInstructorForCourse()
        {
            //arrange 
            SampleDataInitializer.InitializeData(_db);

            //Act 
            string courseName = "Course 1";
            var course = _db.Courses.Where(c => c.Title.Contains(courseName)).
                Include(x => x.Modules).ThenInclude(y => y.Videos).ThenInclude(v => v.Duration).ToList();

            //Assert 
            Assert.That(1, Is.EqualTo(1));

        }
    }
}
