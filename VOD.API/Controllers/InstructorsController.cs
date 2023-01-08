using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VOD.Admin.Service.Services;
using VOD.Admin.Service.Services.Instructors;
using VOD.Common.DTOModels.Admin;

namespace VOD.API.Controllers
{
    [Route("api/[instructors]")]
    [ApiController]
    public class InstructorsController : ControllerBase
    {
        #region
        private readonly IInstructorService _instructorService;
        private readonly LinkGenerator _linkGenerator;
        #endregion

        public InstructorsController(IInstructorService service, LinkGenerator generator)
        {
        }

        [HttpGet()]
        public async Task<ActionResult<List<InstructorDTO>>> Get()
        {
            try
            {
                return await _instructorService.GetInstructorsAsync();
            } catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<InstructorDTO>> Get(int id, bool include = false)
        {
            try
            {
                var dto = await _instructorService.GetInstructorAsync(id);
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
        public async Task<ActionResult<InstructorDTO>> Post(InstructorDTO model)
        {
            try
            {
                if (model == null) return BadRequest("No entity provided");

                var id = await _instructorService.AddInstructorsInfoAsync(model);

                var dto = _instructorService.GetInstructorAsync(id);

                if (dto == null) return BadRequest("Unable to add the entity");

                var uri = _linkGenerator.GetPathByAction("Get", "Instructors", new { id });

                return Created(uri, dto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to add the entity");
            }
        }

        [HttpPut("{id: int}")]
        public async Task<ActionResult<InstructorDTO>> Put(int id, InstructorDTO model) 
        {
            if (!id.Equals(model.Id)) return BadRequest("Differening ids");
            try
            {
                var instructor = await _instructorService.GetInstructorAsync(id);
                if (instructor.Equals(null)) return NotFound("Could not find entity");
                if (await _instructorService.UpdateInstructorsInfoAsync(model) ==1)
                {
                    return NoContent();
                } 
            } 
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update the entity");
            }
            return BadRequest("Unable to update the entity");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var instructorExists = await _instructorService.CheckInstructorExists(id);
                if (!instructorExists) return BadRequest("Could not find entity");

                if (await _instructorService.DeleteInstructorAsync(id) == 1) return NoContent();
            } catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete the entity");
            }
            return BadRequest("Failed to delete the instructor");
        }
    }
}
