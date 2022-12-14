using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VOD.Common.Entities;
using VOD.Database.Contexts;
using VOD.Database.Migrations.DbInitializer;
using VOD.Service.AppStart;
using VOD.Service.CourseServices.Interfaces;
using VOD.Service.DatabaseServices.Concrete;
using VOD.Service.ModulesServices.QueryObjects;
using VOD.Service.UserCoursesService.Concrete;
using VOD.Service.VideosServices.Concrete;
using VOD.Service.VideosServices.Interfaces;
using VOD.UI.HelperExtensions;

namespace VOD.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUserCourseSelectedService, UserCourseSelectedService>();
            services.AddScoped<IListVideoService, ListVideoService>();
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddDefaultIdentity<VODUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<VODContext>();
            services.AddControllersWithViews();
            // Delete this after ensuring everything works without the service.
            // services.AddScoped<IDbWriteService, DbWriteService>();
            services.AddDbContext<VODContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            //services.RegisterServiceLayerDi();
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperConfigProfile());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, IHttpContextAccessor httpContextAccessor)
        {
            // loggerFactory.AddProvider(new RequestTransientLogger(() => httpContextAccessor));

            ModuleListDTOSelect.Configure(app.ApplicationServices.GetService<IMapper>());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().
                    CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetRequiredService<VODContext>();
                    SampleDataInitializer.InitializeData(context);
                }
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
