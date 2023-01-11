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

namespace VOD.Database.Tests.CrudTests
{
    public class DeleteTests: TestBase
    {
        public Action RunTheTest { get; private set; }

        [Test]
        public void ShouldCascadeDelete()
        {
            ExecuteInATransaction(RunTheTest);

            void RunTheTest()
            {
                SampleDataInitializer.ClearData(context);
                var data = TestDataUnits.NewCourseWithInstructorAndModule();
                var result = -1;
                try 
                {
                    context.Courses.Add(data);
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [VOD].[Courses] ON");
                    context.SaveChanges();
                } catch 
                {
                    throw;
                }
                var datatodelete = context.Courses.Include(m => m.Modules).ThenInclude(m => m.Videos).FirstOrDefault();

                int catchCascadeDeleteException() 
                {
                    try
                    { 
                        context.Courses.Remove(datatodelete);
                        result =  context.SaveChanges();
                        return result;
                    }
                    catch (InvalidOperationException ex)
                    //catch IOEx and rethrow as method output to check below.
                    { 
                        throw; 
                    }
                }
                //Assert
                Assert.That(catchCascadeDeleteException, Is.Not.EqualTo(-1));
            }
        }
    }
}
