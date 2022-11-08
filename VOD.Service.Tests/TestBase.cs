using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Database.Contexts;
using VOD.Database.Migrations.DbInitializer;

namespace VOD.Service.Tests
{
    [TestFixture]
    public class TestBase
    {
        protected VODContext context;

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

    }
}
