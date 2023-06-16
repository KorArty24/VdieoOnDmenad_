using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VOD.Admin.Service.Services.Courses;
using VOD.Admin.Service.Services.Instructors;
using VOD.Admin.Service.Services.Videos;
using VOD.API.Filters;
using VOD.API.Services;
using VOD.Common.Entities;
using VOD.Database.Contexts;
using VOD.Service.DatabaseServices;
using VOD.Service.DatabaseServices.Concrete;
using VOD.Service.UserService;
using VOD.Service.UserService.Interfaces;

namespace VOD.API
{
    public class Startup
    {
        private IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddDbContext<VODContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<VODUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>().
                AddEntityFrameworkStores<VODContext>();
            services.AddScoped<IInstructorService, InstructorService>();
            services.AddScoped<ICoursesService, CoursesService>();
            services.AddScoped<IDownloadService, DownloadService>();
            services.AddScoped<IDbReadService, DbReadService>();
            services.AddScoped<IUserService, UserService>();
            services.AddControllers(config => config.Filters.Add(new VodApiExceptionFilter(_env)));
            //services.AddSwaggerDocument();
            services.AddOpenApiDocument(settings =>
            {
                settings.Title = "Catalog API";
                settings.DocumentName = "v3";
                settings.Version = "v3";
            });
            services.AddTransient<ITokenService, TokenService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseOpenApi();
                app.UseSwaggerUi3();
            }
            else
            {
                app.UseExceptionHandler("/Error");

            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseOpenApi().UseSwaggerUi3();
        }
    }
}
