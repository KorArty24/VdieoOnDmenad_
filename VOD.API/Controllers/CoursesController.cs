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
    [Route("api/courses")]
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
            _linkGenerator = generator;
        }
        
        /// <summary>
        /// Gets an existing course
        /// </summary>
        /// <returns>an existing courses</returns>
        /// <response code="200">Returns an existing items </response>
        /// <response code="500">Database failure</response>
        /// <response code="404">Course not found</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
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

        /// <summary>
        /// Gets an existing course
        /// </summary>
        /// <param name="id"></param>
        /// <returns>an existing course</returns>
        /// <response code="200">Returns an existing item </response>
        /// <response code="500">Database failure</response>
        /// <response code="404">Course not found</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
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
        /// <summary>
        /// Creates a new Course
        /// </summary>
        /// <param name="model"></param>
        /// <returns> a newly created Course
        /// </returns>
        /// <response code = "201">Returns a newly created item</response>
        /// <response code = "400">If the item is null</response>
        /// <response code = "500">If error occured</response>
        
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<InstructorDTO>> Post(CourseDTO model)
        {
            try
            {
                if (model == null) return BadRequest("No entity provided");

                var id = await _courseService.AddCourseInfoAsync(model);

                var dto = _courseService.GetCourseAsync(id);

                if (dto == null) return BadRequest("Unable to add the entity");

                var uri = _linkGenerator.GetPathByAction("Get", "Instructors", new {id});

                return Created(uri, dto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to add the entity");
            }
        }

       // public async Task<IActionResult> Put(int id, CourseDTO model)
        
    }
}
