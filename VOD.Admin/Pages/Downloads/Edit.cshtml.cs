using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using VOD.Admin.DTO_Models;
using VOD.Admin.Filters;
using VOD.Admin.Helpers;
using VOD.Admin.Service.Services.Downloads;
using VOD.Admin.Service.Services.Modules;
using VOD.Common.DTOModels.Admin;
using VOD.Service.UserService;
using VOD.Service.UserService.Interfaces;
using DownloadDTO = VOD.Common.DTOModels.Admin.DownloadDTO;

namespace VOD.Admin.Pages.Downloads
{
    [ValidateModel, Authorize(Policy = "AdminOnly")]
    public class EditModel : PageModel
    {
        private readonly IDownloadService _downloadService;
        private readonly IModuleService _moduleService;
        [BindProperty] public DownloadDTO Input { get; set; } =  new DownloadDTO();
        [TempData] public string Alert { get; set; }
        /// <summary>
        /// Create an instance of a  <see cref = CreateModel/>
        /// </summary>
        /// <param name="userService"></param>
        public EditModel(IDownloadService downloadService)
        {
            _downloadService = downloadService;
        }
        public async Task<IActionResult> OnGetAsync(int id, int courseId, int moduleId)
        {
            try 
            {
                ViewData["Modules"] = (await _downloadService.GetDownloadsAsync()).ToSelectList("Id", "CourseAndModule");
                Alert = string.Empty;
                Input = await _downloadService.GetDownloadAsync(id, courseId, moduleId);
                return Page();
            } catch 
            {
                return RedirectToPage("/Index", new { alert = "You do not have access to this page" });
            }
        }

        public async Task<IActionResult> OnPostAsync() 
        {
            var id = Input.ModuleId;
            Input.CourseId = (await _moduleService.GetModulesAsync()).FirstOrDefault(s => s.Id.Equals(id) && s.CourseId.Equals(0)).CourseId;
            var succeeded = await _downloadService.UpdateDownloadInfoAsync(Input);
                if (succeeded == 1)
                {
                    Alert = $"Updated Download {Input.Title} was updated.";
                    return RedirectToPage("Index");
                }
            // redisplay the form if something failed.
            return Page();
        }
    }
}
