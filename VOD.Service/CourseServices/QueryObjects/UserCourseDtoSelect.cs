using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.DTOModels.UI;
using VOD.Common.Entities;
using VOD.Database.Contexts;
using VOD.Database.QueryObjects;
using VOD.Service.CourseServices.QueryObjects;


namespace VOD.Service.CourseServices.QueryObjects
{
    public static class UserCourseDtoSelect
    {
        public static CourseDTO
            CreateCourseCard(UserCourse course)
        {
            return new CourseDTO
            {
                CourseId = course.CourseId,
                CourseTitle = course.Course.Title,
                CourseDescription = course.Course.Description,
                MarqueeImageUrl = course.Course.MarqueeImageUrl,
                CourseImageUrl = course.Course.ImageUrl,
            };
        }
    }
}
