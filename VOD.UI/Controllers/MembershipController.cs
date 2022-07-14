using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Dashboard(SortFilterPageOptions options)
        {
            var listService = new ListCourseService(_context);
            var courselist = await listService.SortCoursePage(options).ToListAsync();

            return View(new CourseListCombinedDto(options, courselist));
        }

        [HttpGet]
        public IActionResult Course (int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Video(int id)
        {
            return View();
        }
    }
}
