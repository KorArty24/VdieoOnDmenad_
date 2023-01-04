using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
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
    }
}
