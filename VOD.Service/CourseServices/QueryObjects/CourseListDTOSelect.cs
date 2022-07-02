using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.DTOModels.UI;
using VOD.Common.Entities;

namespace VOD.Service.CourseServices.QueryObjects
{
    public static class CourseListDTOSelect
    {
        public static IQueryable<CourseWithInstructorAndVideosDTO>
            MapCourseToDTO(this IQueryable<Course> course)
        {
            return course.Select(course => new CourseWithInstructorAndVideosDTO
            {
                CourseId=course.Id,
                CourseTitle=course.Title,
                CourseDescription=course.Description,
                Instructor=course.Instructor.Name,
                //Duration= course.Modules.Select(x => new { Videos = x.Videos.Select(vid => vid.Duration) })
            });
        }

        //private static int GetCourseDuration (Course course)
        //{
        //    List<int> query = (course.Modules.Select(x => new { Videos = x.Videos.Select(vid => vid.Duration) })).ToList();
        //    var sum = query.Sum();

            
        //}
    }
}
