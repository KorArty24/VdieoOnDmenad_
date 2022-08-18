using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.DTOModels.UI;
using VOD.Common.Entities;

namespace VOD.Service.CourseServices.Interfaces
{
    public interface IUserCourseSelectedService
    {
        public Task<CourseDTO> SelectedCoursePageAsync(string userId, int courseId);
        public Task<Course> GetUserCourseSelected(string userId, int courseId);
    }
}
