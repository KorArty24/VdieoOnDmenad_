using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using VOD.Common.Entities;
using VOD.Database.Contexts;
using VOD.Service.CourseServices;
using VOD.Service.CourseServices.Concrete;
using VOD.UI.Models.MembershipViewModels;

namespace VOD.UI.Controllers
{
    public class MembershipController : Controller
    {
        private readonly string _userId;
        private readonly VODContext _context;


        public MembershipController(IHttpContextAccessor httpContextAccessor,
            UserManager<VODUser> userManager, VODContext context)
        {
            var user = httpContextAccessor.HttpContext.User;
            _userId = userManager.GetUserId(user);
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> List(SortFilterPageOptions options)
        {
            var listService = new ListCourseService(_context);
            var courselist = await listService.SortCoursePage(options).ToListAsync();

            return View(new CourseListCombinedDto(options, courselist));
        }

        [HttpGet]
        public JsonResult GetFilterSearchContent 
            (SortFilterPageOptions options) 
        {
            var service = new 
               CourseFilterDropdownService(_context); 

            var traceIdent = HttpContext.TraceIdentifier; 

            return null; //#E
        }

        [HttpGet]
        public async Task <IActionResult> GetSingleCourse (int id)
        {
            var courseservice = new CourseSelectedService(_context);
            var course = await courseservice.SelectedCoursePage(id);
            return View();
        }

        [HttpGet]
        public IActionResult Video(int id)
        {
            return View();
        }
    }
}
