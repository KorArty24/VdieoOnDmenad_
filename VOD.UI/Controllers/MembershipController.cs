using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VOD.Common.DTOModels.UI;
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
        public async Task <IActionResult> Course (string userId, int id)
        {
            var courseservice = new UserCourseSelectedService(_context);
            var course = await courseservice.SelectedCoursePageAsync(userId ,id);
            return View(course);
        }

        [HttpGet]
        public async Task<IActionResult> Video(int id, PageOptions options)
        {
            var videoDto = _videoSelectedService.SelectVideoAsync(_userId,id).Result;
            var courseDto = _userCourseSelectedService.SelectedCoursePageAsync(_userId,id).Result;
            var usercourse = _userCourseSelectedService.GetUserCourseSelected(_userId, id).Result;
            var instructorDto = InstructorDTOSelect.CreateInstructorCard(usercourse.Instructor);
            var course = await _userCourseSelectedService.GetUserCourseSelected(_userId, id);
            var video = _videoSelectedService.SelectVideoEntityAsync(_userId, id).Result;
            var videos = await _listVideoService.GetVideosForUser(_userId, options, video.ModuleId)
                .Result.ToListAsync();  
            var count = videos.Count();
            var index = videos.FindIndex(v => v.Id.Equals(id));
            var previous = videos.ElementAtOrDefault(index - 1);
            var previousId = previous == null? 0: previous.Id;
            var next = videos.ElementAtOrDefault(index +1);
            var nextId = next == null? 0: next.Id;
            var nextTitle = next == null ? string.Empty : next.Title;
            var nextThumb = next == null ? string.Empty : next.Thumbnail;
            var VideoModel = new VideoViewModel
            {
                Video = videoDto,
                Instructor = instructorDto,
                Course = courseDto,
                LessonInfo = new LessonInfoDTO
                {
                    LessonNumber = index + 1,
                    NumberOfLessons = count,
                    NextVideoId = nextId,
                    PreviousVideoId = previousId,
                    NextVideoTitle = nextTitle,
                    NextVideoThumbNail = nextThumb,
                }
            };
            return View(VideoModel);
        }
    }
}
