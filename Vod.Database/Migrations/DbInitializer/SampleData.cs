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
    public static class SampleData
    {
        #region Seeding instructors

        public static IEnumerable<Instructor> GetInstructors() => new List<Instructor>
            {
                new Instructor
                {
                    Name = "John Doe",
                    Description = "Gang of four stole all books",
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
                new Course
                {
                    Id = 1,
                    InstructorId=listOfInstructors[1],
                    Title="Course 1. Foundations of C#",
                    Description= "master the powerful programming language for only 100$ a month",
                    ImageUrl = "images/course2.jpg",
                    MarqueeImageUrl="/images/laptop.jpg",
                    Modules = new List<Module>
                    {
                        new Module
                        {
                            Title = "Module 1. Warm Up",
                            Videos = new List<Video>
                            {
                                new Video
                                {
                                    Title="Video1",
                                    Description="Setting up the enviroment",
                                    Duration=10,
                                    Thumbnail="/images/video1.jpg",
                                    Url="https://www.youtube.com/watch?v=BJFyzpBcaCY"
                                },
                                new Video
                                {
                                    Title="Video2",
                                    Description="Complete Setting up the enviroment",
                                    Duration=15,
                                    Thumbnail="/images/video2.jpg",
                                    Url="https://www.youtube.com/watch?v=stQ1IZEB2xk"
                                }
                            }
                        },
                        //2d Module
                        new Module
                        {
                            Title = "Module2. Fun begins.",
                            Videos= new List<Video>
                            {
                                new Video
                                {
                                    Title ="Video3",
                                    Duration = 20,
                                    Thumbnail="/images/video3.jpg",
                                    Url="https://www.youtube.com/watch?v=GZvSYJDk-us"
                                },
                                new Video
                                {
                                    Title = "Video4. Learn the basics of http",
                                    Duration = 30,
                                    Thumbnail="/images/video4.jpg",
                                    Url = "https://www.youtube.com/watch?v=XU5pw3QRYjQ"
                                }
                            }
                        }
                    }
                },
                new Course
                {
                    InstructorId=listOfInstructors[2],
                    Title="Course 1. Foundations of C#",
                    Description= "master the powerful programming language for only 100$ a month",
                    ImageUrl = "images/course2.jpg",
                    MarqueeImageUrl="/images/laptop.jpg",
                    Modules = new List<Module>
                    {
                       // 3d Module
                        new Module
                        {
                            Title = "Module 3. No time for caution.",
                            Videos = new List<Video>
                            {
                                new Video
                                {
                                    Title="Video5. Saving and loading",
                                    Description="Lesson 7",
                                    Duration=100,
                                    Thumbnail="/images/video5.jpg",
                                    Url="https://www.youtube.com/watch?v=BJFyzpBcaCY"
                                },
                                new Video
                                {
                                    Title="Video6. Version Control",
                                    Description="Version control essentials",
                                    Duration=45,
                                    Thumbnail="/images/video6.jpg",
                                    Url="https://www.youtube.com/watch?v=JTE2Fn_sCZs"
                                }
                            }
                        }
                    }
                },
                new Course
                {
                    InstructorId=listOfInstructors[3],
                    Title="Course 3. C# in depth",
                    Description= "master hidden depths of C#",
                    ImageUrl = "images/course3.jpg",
                    MarqueeImageUrl="/images/laptop.jpg",
                    Modules = new List<Module>
                    {
                        //4th Module
                        new Module
                        {
                            Title = "Module 4. Working with memory.",
                            Videos = new List<Video>
                            {
                                new Video
                                {
                                    Title="Video7. Memory and all that staff",
                                    Description="Lesson 8",
                                    Duration=60,
                                    Thumbnail="/images/video7.jpg",
                                    Url="https://www.youtube.com/watch?v=BJFyzpBcaCY"
                                },
                                new Video
                                {
                                    Title="Video8. Garbage collection ",
                                    Description="Garbage collection essentials",
                                    Duration=45,
                                    Thumbnail="/images/video8.jpg",
                                    Url="https://www.youtube.com/watch?v=UnaNQgzw4zY"
                                }
                            }
                        }
                    }
                }
            };
            return courses;
        }
        #endregion

        #region Seeding Downloads
        public static IEnumerable<Download> GetDownloads(VODContext context)
        {
            var listOfModuleIds = new List<int>
            {
                context.Modules.First().Id,
                context.Modules.Skip(1).FirstOrDefault().Id,
                context.Modules.Skip(2).FirstOrDefault().Id,
                context.Modules.Skip(3).FirstOrDefault().Id

            };

            var listOFCourseIds = new List<int>
            {
                context.Courses.First().Id,
                context.Courses.Skip(1).FirstOrDefault().Id,
                context.Courses.Skip(2).FirstOrDefault().Id
            };

            var downloads = new List<Download>()
            {
                new Download {ModuleId= listOfModuleIds[1], CourseId = listOFCourseIds[1], Title = "ADO.NET 1 (PDF)", Url= "https://some-url" },
                new Download {ModuleId = listOfModuleIds[2], CourseId = listOFCourseIds[2], Title = "Phill Japikse PRO C# (PDF)", Url="https://some-url"},
                new Download {ModuleId= listOfModuleIds[3], CourseId = listOFCourseIds[3], Title ="Sedgewik (PDf)", Url = "https://some-url"},
                new Download {ModuleId= listOfModuleIds[4], CourseId = listOFCourseIds[3], Title = "ADO.NET 1 (PDF)", Url="https://some-url" }

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
                    usercourses.AddRange
                        (
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
                        }
                        );
                }
            }
            return usercourses;
        }

        #endregion
    }
}
