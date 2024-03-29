﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Database.Contexts;


namespace VOD.Database.Migrations.DbInitializer
{
    public static class SampleDataInitializer
    {
        public static void DropAndCreateDatabase(VODContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.Migrate();
        }

        internal static void ResetIdentity(VODContext context)
        {
            var tables = new[] {"Instructors", "Courses", "Modules", "Videos", "Downloads"};

            foreach (var table in tables)
            {
                var rawSqlString = $"DBCC CHECKIDENT (\"VOD.{table}\", RESEED, 1);";
#pragma warning disable EF1000 // Possible SQL injection vulnerability.
                context.Database.ExecuteSqlRaw(rawSqlString);
#pragma warning restore EF1000 // Possible SQL injection vulnerability.
            }
        }

        public static void ClearData(VODContext context) 
        {
            context.Database.ExecuteSqlRaw("Delete from VOD.UserCourses");
            context.Database.ExecuteSqlRaw("Delete from VOD.Downloads");
            context.Database.ExecuteSqlRaw("Delete from VOD.Videos");
            context.Database.ExecuteSqlRaw("Delete from VOD.Modules");
            context.Database.ExecuteSqlRaw("Delete from VOD.Courses");
            context.Database.ExecuteSqlRaw("Delete from VOD.Instructors");
            context.Database.ExecuteSqlRaw("Delete from VOD.AspNetUserClaims");
            context.Database.ExecuteSqlRaw("Delete from VOD.AspNetUsers");
            ResetIdentity(context);
        }

        internal static void SeedData(VODContext context)
        {
            try
            {
                if (!context.Instructors.Any())
                {
                    context.Database.OpenConnection();
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [VOD].[INSTRUCTORS] ON");
                    context.Instructors.AddRange(SampleData.GetInstructors());
                    context.SaveChanges();
                    context.Database.CloseConnection();
                }
                if (!context.Courses.Any())
                {
                    context.Database.OpenConnection();
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [VOD].[Courses] ON");
                    context.Courses.AddRange(SampleData.GetCourses(context));
                    context.SaveChanges();
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [VOD].[Courses] OFF");
                }
                if (!context.Modules.Any())
                {
                    context.Modules.AddRange(SampleData.GetModules(context));
                    context.SaveChanges();
                }
                if (!context.Downloads.Any()) 
                {
                    context.AddRange(SampleData.GetDownloads(context));
                    context.SaveChanges();
                }
                if (!context.Users.Any()) 
                {
                    context.Users.AddRange(SampleUserData.GetUsers());
                    context.SaveChanges();
                }
                if (!context.UserCourses.Any()) 
                {
                    context.UserCourses.AddRange(SampleData.GetUserCourses(context));
                    context.SaveChanges();
                }
                if (!context.UserClaims.Any())
                {
                    context.UserClaims.AddRange(SampleUserData.GetUserClaims());
                    context.SaveChanges();
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
            }
        }

        public static void InitializeData(VODContext context) 
        {
            //Ensure the database exists and is up to date
            context.Database.Migrate();
            ClearData(context);
            SeedData(context);
        }
    }
}
