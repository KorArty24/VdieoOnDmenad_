using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.DTOModels.UI;
using VOD.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace VOD.Service.CourseServices.QueryObjects
{
    public static class CourseListDTOSelect
    {
        public static IQueryable<CourseWithInstructorAndVideosDTO>
            MapCourseToDTO(this IQueryable<Course> courses)
        {
            return courses.Select(course => new CourseWithInstructorAndVideosDTO
            {
                CourseId = course.Id,
                CourseTitle = course.Title,
                CourseDescription = course.Description,
                Instructor = course.Instructor.Name,
                Duration = course.Modules.SelectMany(m => m.Videos).Select(v => v.Duration).Count()
                //Duration =
                //(from v in videos
                // let id = course.Id
                // where (v.CourseId == id)
                // select v.Duration).Sum()
            });
        }

    }
}
