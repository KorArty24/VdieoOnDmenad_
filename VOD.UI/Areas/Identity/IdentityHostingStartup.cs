using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VideoOnDemand.VOD.Database;

[assembly: HostingStartup(typeof(VOD.UI.Areas.Identity.IdentityHostingStartup))]
namespace VOD.UI.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<Contexts>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("ContextsConnection")));

               // services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                 //   .AddEntityFrameworkStores<Contexts>();
            });
        }
    }
}