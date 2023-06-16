using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using VOD.Admin.Service.Services.Videos;
using VOD.Common.DTOModels.Admin;

namespace VOD.Admin.Pages.Downloads
{
    [Authorize("AdminOnly")]
    public class IndexModel : PageModel
    {
        #region Properties
        private readonly IDownloadService _downloadService;
        public IEnumerable<DownloadDTO> Items = new List<DownloadDTO>();
        [TempData] public string Alert { get; set; }

        #endregion

        #region Constructor 
        /// <summary>
        /// Create instance of a <see cref="IndexModel"/>.
        /// </summary>
        /// <param name ="downloadservice"> Instance of <inheritdoc cref="IDownloadService"/>.</param>
        public IndexModel(IDownloadService downloadservice)
        {
            _downloadService = downloadservice;
        }
        #endregion
        public async Task<IActionResult> OnGetAsync()
        {
          try
            {
                Items = await _downloadService.GetDownloadAsync();
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
