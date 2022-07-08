using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Database.Migrations.DbInitializer;
using VOD.Database.Tests.Base;

namespace VOD.Database.Tests.CrudTests
{
    public class DeleteTests: TestBase
    {
        public Action RunTheTest { get; private set; }


        [Test]
        public void ShouldFailToCascadeDelete()
        {
            ExecuteInATransaction(RunTheTest);

            void RunTheTest()
            {
                SampleDataInitializer.ClearData(context);
                var data = TestDataUnits.NewCourseWithInstructorAndModule();
                context.Courses.Add(data);
                context.SaveChanges();
                var datatodelete = context.Courses.Include(m => m.Modules).
                    First();

                void catchCascadeDeleteException() 
                {
                    try { context.Courses.Remove(datatodelete); }
                    catch (InvalidOperationException ex)
                    //catch IOEx and rethrow as method output to check below.
                    { throw; }
                    
                }
                //Assert
                Assert.Throws<System.InvalidOperationException>(catchCascadeDeleteException);
            }
        }
    }
}
