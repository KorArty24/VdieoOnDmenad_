using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Threading.Tasks;
using VOD.Admin.Filters;
using VOD.Common.DTOModels.Admin;
using VOD.Service.UserService.Interfaces;
using VOD.Admin.Service.Services.Downloads;
using VOD.Admin.Helpers;
using VOD.Admin.Service.Services.Modules;
using VOD.Common.Entities;

namespace VOD.Admin.Pages.Downloads
{
    [ValidateModel, Authorize(Policy = "AdminOnly")]
    public class CreateModel : PageModel
    {
        private readonly IDownloadService _downloadService;
        private readonly IModuleService _moduleService;
        [BindProperty] public DownloadDTO Input { get; set; } =  new DownloadDTO();
        [TempData] public string Alert { get; set; }
        /// <summary>
        /// Create an instance of a  <see cref = EditModel/>
        /// </summary>
        /// <param name="userService"></param>
        public CreateModel(IDownloadService downloadService)
        {
           _downloadService = downloadService;
        }
        public async Task<IActionResult> OnGetAsync(int downloadId)
        {
            try
            {
                 ViewData["Modules"] = (await _downloadService.GetDownloadsAsync()).ToSelectList("Id", "CourseAndModule");
                 return Page();
            } catch
            {
                return RedirectToPage("/Index", new { alert = "You do not have access to this page." });
            }         
        }
        
        public async Task<IActionResult> OnPostAsync() 
        {
            var id = Input.ModuleId;
            int _id = (await _moduleService.GetModuleAsync(id)).CourseId;
            Input.CourseId = _id;
            var result = await _downloadService.AddDownloadInfoAsync(Input);
                var succeeded = result > 0;
                if (succeeded)
                {
                    Alert = $"Created a new Download for {Input.Title}.";
                    return RedirectToPage("Index");
                }
            // Something failed, redisplay the form.
            ViewData["Modules"] = (await _moduleService.GetModulesAsync()).ToSelectList("Id", "CourseAndModules");
            return Page();
        }
    }
}
