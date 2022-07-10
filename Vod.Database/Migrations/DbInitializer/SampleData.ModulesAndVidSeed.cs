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
        #region Seeding Modules and Videos
        public static IEnumerable<Module> GetModules(VODContext context)
        {
            int numOfCourses = context.Courses.Count();
            var listOfCourseIds = new List<int>();
            for (int i = 0; i < numOfCourses; i++)
            {
                var res = context.Courses.Skip(i).FirstOrDefault().Id;
                listOfCourseIds.Add(res);
            };

            var modules = new List<Module>
            {
                new Module
                //1st module
                {
                    CourseId=listOfCourseIds[0], // Course 1
                    Title = "Module 1. Warm Up",
                    Videos = new List<Video>
                    {
                        new Video
                        {
                            CourseId=listOfCourseIds[0],
                            Title="Video1",
                            Description="Setting up the enviroment",
                            Duration=10,
                            Thumbnail="/images/video1.jpg",
                            Url="https://www.youtube.com/watch?v=BJFyzpBcaCY"
                        },
                        new Video
                        {
                            CourseId=listOfCourseIds[0],
                            Title="Video2",
                            Description="Complete Setting up the enviroment",
                            Duration=15,
                            Thumbnail="/images/video2.jpg",
                            Url="https://www.youtube.com/watch?v=stQ1IZEB2xk"
                        }
                    }
                },
                //Module 2.1 
                new Module
                {
                    CourseId=listOfCourseIds[1],
                    Title = "Module2. Fun begins.",
                    Videos= new List<Video>
                    {
                        new Video
                        {
                            CourseId=listOfCourseIds[1],
                            Title ="Video3",
                            Description="Lesson 3",
                            Duration = 20,
                            Thumbnail="/images/video3.jpg",
                            Url="https://www.youtube.com/watch?v=GZvSYJDk-us"
                        },
                        new Video
                        {
                            CourseId=listOfCourseIds[1],
                            Title = "Video4. Learn the basics of http",
                            Description="Lesson 4",
                            Duration = 30,
                            Thumbnail="/images/video4.jpg",
                            Url = "https://www.youtube.com/watch?v=XU5pw3QRYjQ"
                        }
                    }
                },
                // Module 2.2
                 new Module
                {
                    CourseId=listOfCourseIds[1],
                    Title = "Module 3. Roasted on Linqs",
                    Videos = new List<Video>
                    {
                        new Video
                        {
                            CourseId=listOfCourseIds[1],
                            Title="Video6. Linq in Action",
                            Description="Lesson 6",
                            Duration=100,
                            Thumbnail="/images/video6.jpg",
                            Url="https://www.youtube.com/watch?v=yClSNQdVD7g"
                        },
                        new Video
                        {
                            CourseId=listOfCourseIds[1],
                            Title="Video7. Linq in depth",
                            Description="Dive into expression trees, fluent syntax, joining and set specific queries",
                            Duration=35,
                            Thumbnail="/images/video7.jpg",
                            Url="https://www.youtube.com/watch?v=sIXKpyhxHR8"
                        }
                    }
                },

                //3.1 module
                new Module
                {
                    CourseId=listOfCourseIds[2],
                    Title = "Module 3. No time for caution.",
                    Videos = new List<Video>
                    {
                        new Video
                        {
                            CourseId=listOfCourseIds[2],
                            Title="Video5. Saving and loading",
                            Description="Lesson 5",
                            Duration=100,
                            Thumbnail="/images/video5.jpg",
                            Url="https://www.youtube.com/watch?v=BJFyzpBcaCY"
                        },
                        new Video
                        {
                            CourseId=listOfCourseIds[2],
                            Title="Video6. Version Control",
                            Description="Version control essentials",
                            Duration=45,
                            Thumbnail="/images/video6.jpg",
                            Url="https://www.youtube.com/watch?v=JTE2Fn_sCZs"
                        }
                    }
                }
            };
            return modules;
        }
        #endregion
    }
}
