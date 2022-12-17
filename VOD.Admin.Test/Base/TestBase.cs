using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Database.Contexts;
using VOD.Database.Migrations.DbInitializer;

namespace VOD.Admin.Tests.Base
{
    [TestFixture]
    public class TestBase : WebApplicationFactory<VOD.Admin.Startup>
    {
        protected VODContext context;
        protected WebApplicationFactory<VOD.Admin.Startup> _factory;
        protected HttpClient _client;

        [SetUp]
        public void SetUp()
        {
            context = new VODContextFactory().CreateDbContext(new string[0]);
            SampleDataInitializer.InitializeData(context);
        }

        [TearDown]
        public void CleanDatabase()
        {
            SampleDataInitializer.ClearData(context);
        }

        protected void ExecuteInATransaction(Action actionToExecute)
        {
            var strategy = context.Database.CreateExecutionStrategy();
            strategy.Execute(() =>
            {
                using (var trans = context.Database.BeginTransaction())
                {
                    actionToExecute();
                    trans.Rollback();
                }
            });
        }
        protected void CreateCourseGraph()
        {
            CreateCourseGraph(context);
        }

        protected void CreateCourseGraph(VODContext context)
        {
            SampleDataInitializer.ClearData(context);
            context.AddRange(SampleData.GetInstructors());
            context.SaveChanges();
            context.AddRange(SampleData.GetCourses(context));
            context.SaveChanges();
            context.AddRange(SampleData.GetModules(context));
            context.SaveChanges();
            context.AddRange(SampleData.GetDownloads(context));
            context.SaveChanges();
        }
    }
}
