using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.Entities;
using VOD.Database.Contexts;

namespace VOD.Service.AdminServices.Concrete
{
    internal class AddNewCourseService : IAddNewCourseService
    {
        private readonly VODContext _context;
        public AddNewCourseService(VODContext context)
        {
            _context = context;
        }

        public string InstructorName => throw new NotImplementedException();

        public Course AddCourse(Course course)
        {
            throw new NotImplementedException();
        }

        //public Course AddCourse(Course course)
        //{
        //    int Id = 
        //    return new Course
        //    {

        //    }
        //}
    }
}
