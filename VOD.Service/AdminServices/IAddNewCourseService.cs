using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.Entities;

namespace VOD.Service.AdminServices
{
    public interface IAddNewCourseService
    {
        string InstructorName { get; }

        Course AddCourse(Course course);
    }
}
