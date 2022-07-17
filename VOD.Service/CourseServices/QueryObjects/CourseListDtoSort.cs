using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.DTOModels.UI;

namespace VOD.Service.CourseServices.QueryObjects
{
     public enum OrderByOptions
    {
        [Display(Name = "sort by...")] SimpleOrder = 0,
        [Display(Name = "Duration ↑")] ByDuration,
        [Display(Name = "Title ↑")] ByTitle
    }

    public static class CourseListDtoSort
    {
        
        public static IQueryable<CourseWithInstructorAndVideosDTO> OrderCoursesBy
            (this IQueryable<CourseWithInstructorAndVideosDTO> courses, OrderByOptions orderByOptions)
        {
            switch(orderByOptions)
            {
                case OrderByOptions.SimpleOrder:
                    return courses.OrderByDescending(x => x.CourseId);
                case OrderByOptions.ByDuration:
                    return courses.OrderByDescending(x=> x.Duration);
                case OrderByOptions.ByTitle:
                    return courses.OrderByDescending(x=> x.CourseTitle);
                default:
                    throw new ArgumentOutOfRangeException
                        (nameof(orderByOptions), orderByOptions, null);
            }
        }
    }
}
