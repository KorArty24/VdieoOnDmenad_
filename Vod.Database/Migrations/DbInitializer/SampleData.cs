using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Database.Contexts;
using VOD.Common.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity;

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
                },
                new Instructor
                { 
                    Name = "Phillip Japikse",
                    Description = "Dotnet guru",
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
                context.Instructors.Skip(2).FirstOrDefault().Id,
                context.Instructors.Skip(3).FirstOrDefault().Id
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
            const int NumUsers = 3; // Number of test users see class SampleUserData
            const int NumSampleUserCourses = 3;
            var emails = new Stack<string>();
            for (int i = 0; i < NumUsers; i++)
            {
                emails.Push(context.Users.Skip(i).FirstOrDefault().Email);
            }
            string email = null;
            var adminRoleId = string.Empty;
            string userId = null;
            var userIds = new Stack<string>();
            while(emails.Count() > 0) 
            {
                if (context.Users.Count().Equals(NumSampleUserCourses))
                {
                    userIds.Push(context.Users.First(r => r.Email.Equals(emails.Pop())).Id);
                }
                else
                {
                    throw new System.NotImplementedException("dfs");
                };
            }
            var usercourses = new List<UserCourse>();
            for (int i = 0; i < NumSampleUserCourses; i++)
            { 
                if (!userIds.TryPop(out userId).Equals(string.Empty))
                {
                    if (!context.UserCourses.Any())
                    {
                        usercourses.Add(
                            new UserCourse 
                            {
                                UserId=userId,
                                CourseId=context.Courses.Skip(i).FirstOrDefault().Id
                            });
                    }
                }
            }
            return usercourses;
        }
        #endregion
    }
}
