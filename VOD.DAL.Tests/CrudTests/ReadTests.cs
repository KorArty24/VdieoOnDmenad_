using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Database.Migrations.DbInitializer;
using VOD.Database.Tests.Base;
using Microsoft.EntityFrameworkCore;
using VOD.Database.Contexts;

namespace VOD.Database.Tests.CrudTests
{
    public class ReadTests: TestBase
    {
        protected VODContext _db
        {
            get { return this.context; }
        }
        
        [Test]
        [TestCase("Asynchronous")]
        public void ShouldGetAllVideosForFilteredCourse(string Title) 
        {
            ExecuteInATransaction(RunTheTest);

            void RunTheTest()
            {
                //Arrange
                CreateCourseGraph();
                //Act
                var videos = _db.Videos.AsNoTracking()
                    .Where(v=> v.Course.Title.Contains(Title)).ToList();
                //Assert 
                Assert.AreEqual(4, videos.Count());
            }
        }

        [Test]
        public void ShouldEagerlyLoadRelatedData()
        {
            ExecuteInATransaction(RunTheTest);
            void RunTheTest()
            {
                CreateCourseGraph();
                var data = _db.Courses.AsNoTracking().
                    Include(x => x.Modules).ThenInclude(t => t.Downloads).ToList();
                Assert.AreEqual(1, data[0].Modules.Count());
            }
        }

        [Test]
        public void ShouldGetDataWithFromSql()
        {
            ExecuteInATransaction(RunTheTest);

            void RunTheTest()
            {
                CreateCourseGraph();
                var videoTitle = _db.Videos.
                    FromSqlRaw("Select * from VOD.Videos").Include(x=>x.Module).ToList();
            }
        }
    }
}
