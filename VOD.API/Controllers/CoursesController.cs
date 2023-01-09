using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Threading.Tasks;
using VOD.Admin.Service.Services.Courses;
using VOD.Admin.Service.Services.Instructors;
using VOD.Common.DTOModels.Admin;

namespace VOD.API.Controllers
{
    [Route("api/[courses]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        #region
        private readonly ICoursesService _courseService;
        private readonly LinkGenerator _linkGenerator;
        #endregion

        public CoursesController(ICoursesService service, LinkGenerator generator)
        {
            _courseService = service;
        }

        [HttpGet()]
        public async Task<ActionResult<List<CourseDTO>>> Get()
        {
            try
            {
                return await _courseService.GetCoursesAsync();
            } catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{int:id}")]
        public async Task<ActionResult<CourseDTO>> Get(int id)
        {
            try
            {
                var dto = await _courseService.GetCourseAsync(id);
                if (dto == null)
                {
                    return NotFound();
                }
                return dto;
            } catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<InstructorDTO>> Post(CourseDTO model)
        {
            try
            {
                if (model == null) return BadRequest("No entity provided");

                var id = await _courseService.AddCourseInfoAsync(model);

                var dto = _courseService.GetCourseAsync(id);

                if (dto == null) return BadRequest("Unable to add the entity");

                var uri = _linkGenerator.GetPathByAction("Get", "Instructors", new { id });

                return Created(uri, dto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to add the entity");
            }
        }
        
    }
}
