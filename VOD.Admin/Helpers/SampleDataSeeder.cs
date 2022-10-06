using Microsoft.EntityFrameworkCore;
using VOD.Database.Contexts;
using VOD.Database.Migrations.DbInitializer;

namespace VOD.Admin.Helpers
{
    public class SampleDataSeeder
    {
        protected VODContext context;

        public void SeedData()
        {
            context = new VODContextFactory().CreateDbContext(new string[0]);
            SampleDataInitializer.InitializeData(context);
        }
    }
}
