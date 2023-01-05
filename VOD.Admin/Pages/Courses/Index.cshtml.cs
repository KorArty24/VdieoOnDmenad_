using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using VOD.Common.DTOModels.Admin;
using VOD.Service.UserService.Interfaces;

namespace VOD.Admin.Pages.Instructors
{
    [Authorize("AdminOnly")]
    public class IndexModel : PageModel
    {
        #region Properties
        private readonly IUserService _userService;
        public IEnumerable<InstructorDTO> Users = new List<InstructorDTO>();
        [TempData] public string Alert { get; set; }

        #endregion

        #region Constructor 
        /// <summary>
        /// Create instance of a <see cref="IndexModel"/>.
        /// </summary>
        /// <param name ="userservice"> Instance of <inheritdoc cref="IUserService"/>.</param>
        public IndexModel(IUserService userservice)
        {
            _userService = userservice;
        }
        #endregion
        public async Task OnGetAsync()
        {
           // Users = await _userService.GetUsersAsync();
        }
    }

}
