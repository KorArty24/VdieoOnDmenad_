using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using VOD.Common.Entities;
using VOD.Database.Contexts;
using VOD.Service.CommonOptions;
using VOD.Service.CourseServices;
using VOD.Service.CourseServices.Concrete;
using VOD.Service.CourseServices.Interfaces;
using VOD.Service.InstructorServices.QueryObjects;
using VOD.Service.UserCoursesService.Concrete;
using VOD.Service.VideosServices.Concrete;
using VOD.Service.VideosServices.Interfaces;
using VOD.UI.Models.MembershipViewModels;

namespace VOD.UI.Controllers
{
    public class MembershipController : Controller
    {
        private readonly string _userId;
        private readonly VODContext _context;
        private readonly IVideoSelectedService _videoSelectedService;
        private readonly IUserCourseSelectedService _userCourseSelectedService;
        private readonly IListVideoService _listVideoService;


        public MembershipController(IHttpContextAccessor httpContextAccessor,
            UserManager<VODUser> userManager, VODContext context,
            IVideoSelectedService videoSelectedService, IUserCourseSelectedService userCourseSelectedService,
            IListVideoService listVideoService)
        {
            var user = httpContextAccessor.HttpContext.User;
            _userId = userManager.GetUserId(user);
            _context = context;
            _videoSelectedService = videoSelectedService;
            _userCourseSelectedService = userCourseSelectedService;
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
        public async Task <IActionResult> Course (string userId,int id)
        {
            var courseservice = new UserCourseSelectedService(_context);
            var course = await courseservice.SelectedCoursePageAsync(userId ,id);
            return View(course);
        }

        [HttpGet]
        public async IActionResult Video(int id, PageOptions options)
        {
            var videoDto = _videoSelectedService.SelectedVideoAsync(_userId,id).Result;
            var courseDto = _userCourseSelectedService.SelectedCoursePageAsync(_userId,id).Result;
            var usercourse = _userCourseSelectedService.GetUserCourseSelected(_userId, id).Result;
            var instructorDto = InstructorDTOSelect.CreateInstructorCard(usercourse.Instructor);
            var course = await _userCourseSelectedService.GetUserCourseSelected(_userId, id);
            var videos = await _listVideoService.GetVideoPage(options).Result.ToListAsync();  



        }
    }
}
