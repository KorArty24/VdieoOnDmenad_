using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VOD.Admin.Filters;
using VOD.Admin.Service.Services.Downloads;
using VOD.Admin.Service.Services.Videos;
using VOD.Common.DTOModels;
using VOD.Common.DTOModels.Admin;
using VOD.Service.UserService.Interfaces;

namespace VOD.Admin.Pages.Downloads
{
    [ValidateModel, Authorize(Policy = "AdminOnly")]
    public class DeleteModel : PageModel
    {
        #region Properties
        private readonly IDownloadService _downloadService;
        [BindProperty] public DownloadDTO Input { get; set; } = new DownloadDTO();
        [TempData] public string Alert { get; set; }
        #endregion

        #region Constructor
        public DeleteModel(IDownloadService downloadService)
        {
            _downloadService = downloadService;
        }
        #endregion

        #region Actions
        public async Task<IActionResult> OnGetAsync(int id, int courseId, int moduleId)
        {
            try
            {
                 Input = await _downloadService.GetDownloadAsync(id, courseId, moduleId);
                return Page();
            } catch
            {
                return RedirectToPage("/Index", new
                {
                    alert = "You do not have access to this page."
                });
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            int id = Input.Id, moduleid = Input.ModuleId, courseid = Input.CourseId; 
            
            if (ModelState.IsValid)
            {
                var succeeded = await _downloadService.DeleteDownloadAsync(id, moduleid, courseid);
                if (succeeded==1)
                {
                    Alert = $"Deleted Download: {Input.Title}.";

                    return RedirectToPage("Index");
                }
            }
                Input = await _downloadService.GetDownloadAsync(moduleid, courseid, courseid);
                return Page();
        }
        #endregion
    }
}
