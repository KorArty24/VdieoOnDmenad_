using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Database.Contexts;
using VOD.Common.Entities;

namespace VOD.Database.Migrations.DbInitializer
{
    public static partial class SampleData
    {
        #region Seeding instructors

        public static IEnumerable<Instructor> GetInstructors() => new List<Instructor>
            {
                new Instructor
                {
                    Name = "John Doe",
                    Description = "Stole all books With Gang of Four",
                    Thumbnail = "/images/Ice-Age-Scrat-icon.png"
                },
                new Instructor
                {
                    Name ="Bob Martin",
                    Description = "Learn all datastructures in one hour!",
                    Thumbnail= "/images/Ice-Age-Scrat-icon.png"
                },
                new Instructor
                {
                    Name = "Cercei Lannister",
                    Description = "Cercei choose violence",
                    Thumbnail = "images/Ice-Age-Scrat-icon.png"
                }
            };
        #endregion

        #region Seeding courses, modules, Videos.

        public static IEnumerable<Course> GetCourses(VODContext context)  
        {
           
           var listOfInstructors = new List<int>
            {
                context.Instructors.First().Id,
                context.Instructors.Skip(1).FirstOrDefault().Id,
                context.Instructors.Skip(2).FirstOrDefault().Id
            };

            var courses = new List<Course>
            {
                //1st
                new Course
                {
                    InstructorId=listOfInstructors[0],
                    Title="Course 1. Foundations of C#",
                    Description= "master the powerful programming language for only 100$ a month",
                    ImageUrl = "images/course1.jpg",
                    MarqueeImageUrl="/images/laptop.jpg",
                },
                //2d
                new Course
                {
                    InstructorId=listOfInstructors[1],
                    Title="Concurency and Asynchronous programming in C#",
                    Description= "become an expert in multithreading and async programming",
                    ImageUrl = "images/course2.jpg",
                    MarqueeImageUrl="/images/laptop.jpg",
                },
                //3d
                new Course
                {
                    InstructorId=listOfInstructors[2],
                    Title="Course 3. C# in depth",
                    Description= "master hidden depths of C#",
                    ImageUrl = "images/course3.jpg",
                    MarqueeImageUrl="/images/laptop.jpg",
                }
            };
            return courses;
        }
        #endregion


        #region Seeding Downloads
        public static IEnumerable<Download> GetDownloads(VODContext context)
        {
            var listOfModuleIds = new List<int>();

            for (int i=0; i<context.Modules.Count(); i++)
            {
                var res = context.Modules.Skip(i).FirstOrDefault().Id;
                listOfModuleIds.Add(res);
            };
            
            var listOFCourseIds = new List<int>
            {
                context.Courses.First().Id,
                context.Courses.Skip(1).FirstOrDefault().Id,
                context.Courses.Skip(2).FirstOrDefault().Id
            };

            var downloads = new List<Download>()
            {
                new Download {ModuleId = listOfModuleIds[0], CourseId = listOFCourseIds[0], Title = "ADO.NET 1 (PDF)", Url= "https://some-url" },
                new Download {ModuleId = listOfModuleIds[1], CourseId = listOFCourseIds[1], Title = "Phill Japikse PRO C# (PDF)", Url="https://some-url"},
                new Download {ModuleId = listOfModuleIds[2], CourseId = listOFCourseIds[2], Title ="Sedgewik (PDf)", Url = "https://some-url"},
                new Download {ModuleId = listOfModuleIds[2], CourseId = listOFCourseIds[2], Title = "ADO.NET 1 (PDF)", Url="https://some-url" }
            };
            return downloads;
        }

        #endregion

        #region Seed UserCourses
        public static IEnumerable<UserCourse> GetUserCourses(VODContext context)
        {
            var email = "bobbySinger@yahoo.com";
            var adminRoleId = string.Empty;
            var userId = string.Empty;
            if (context.Users.Any(r => r.Email.Equals(email))) 
            {
                userId = context.Users.First(r => r.Email.Equals(email)).Id;

            }
            var usercourses = new List<UserCourse>();
            if (!userId.Equals(string.Empty))
            {
                if (!context.UserCourses.Any())
                {
                    usercourses.AddRange(
                    new List<UserCourse> 
                    {
                        new UserCourse 
                        {
                            UserId=userId,
                            CourseId=context.Courses.First().Id
                        },
                        new UserCourse
                        {
                            UserId=userId,
                            CourseId=context.Courses.Skip(1).FirstOrDefault().Id
                        },                          
                        new UserCourse
                        {
                            UserId=userId,
                            CourseId= context.Courses.Skip(2).FirstOrDefault().Id
                        }
                    });
                }
            }
            return usercourses;
        }
        #endregion
    }
}
