using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using VOD.Admin.Service.Services.Videos;
using VOD.Common.DTOModels.Admin;

namespace VOD.Admin.Pages.Videos
{
    [Authorize("AdminOnly")]
    public class IndexModel : PageModel
    {
        #region Properties
        private readonly IVideoService _videoService;
        public IEnumerable<VideoDTO> Items = new List<VideoDTO>();
        [TempData] public string Alert { get; set; }

        #endregion

        #region Constructor 
        /// <summary>
        /// Create instance of a <see cref="IndexModel"/>.
        /// </summary>
        /// <param name ="videoservice"> Instance of <inheritdoc cref="IVideoService"/>.</param>
        public IndexModel(IVideoService videoservice)
        {
            _videoService = videoservice;
        }
        #endregion
        public async Task<IActionResult> OnGetAsync()
        {
          try
            {
                Items = await _videoService.GetVideosAsync();
                return Page();
            }
            catch
            {
                Alert = "You do not have access to this page.";
                return RedirectToPage("/Index");
            }
        }
    }

}
