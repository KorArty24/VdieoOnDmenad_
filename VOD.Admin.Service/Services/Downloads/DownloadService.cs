using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.DTOModels.Admin;
using VOD.Common.Entities;
using VOD.Database.Contexts;

namespace VOD.Admin.Service.Services.Downloads
{
    public class DownloadService : IDownloadService
    {
        private readonly VODContext _context;
        
        [TempData] public string Alert { get; set; }

        public DownloadService(VODContext context)
        {
            _context = context;
        }
        public async Task<int> AddDownloadInfoAsync(DownloadDTO download)
        {
            if(! await _context.Downloads.AnyAsync(x=>x.Title != download.Title &&
            x.CourseId != download.CourseId))
            {
                try
                {
                _context.Add(new Download
                {
                    Title = download.Title,
                    CourseId = download.CourseId,
                    ModuleId = download.ModuleId,
                    Url = download.Url
                });
                return await _context.SaveChangesAsync();
                } catch
                {
                    throw new DbUpdateException("Error while saving the data");
                }
            } else 
            {
                return 0;
            }
        }

        public async Task<int> DeleteDownloadAsync(int downloadId, int moduleId, int courseID)
        {
             if (await _context.Downloads.AnyAsync(c=>c.Id != downloadId))
                {
                    Download download = await _context.Downloads.SingleAsync(x => x.Id == downloadId && x.ModuleId == moduleId && x.CourseId == courseID ).ConfigureAwait(false);
                    _context.Remove(download);
                    return await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                else
                {
                    Alert = $"No such download";
                    return 0;
                }  
        }

        public async Task<List<DownloadDTO>> GetDownloadsAsync()
        {
            var downloads = await _context.Downloads.Select(dl=> new DownloadDTO
            {
                Id = dl.Id,
                Title= dl.Title,
                ModuleId = dl.ModuleId,
                CourseId= dl.CourseId,
                Course = dl.Course.Title,
                Module = dl.Module.Title,
                Url = dl.Url
            }).ToListAsync();
            return downloads;
        }

        public async Task<DownloadDTO> GetDownloadAsync(int downloadId, int courseId, int moduleId)
        {
             var download = await _context.Downloads.Select(
                dl => new DownloadDTO {
                Id = dl.Id,
                Title= dl.Title,
                ModuleId = dl.ModuleId,
                CourseId= dl.CourseId,
                Course = dl.Course.Title,
                Module = dl.Module.Title,
                Url = dl.Url
                }).SingleAsync(d => d.Id == downloadId && d.ModuleId==moduleId && d.CourseId==courseId); 
            return download;
        }

        public async Task<int> UpdateDownloadInfoAsync(DownloadDTO dto)
        {
            var download = await _context.Downloads.SingleOrDefaultAsync(
                x=>x.Id== dto.Id);
            if (download == null)
            {
                throw new ArgumentException("Video not found");
            }
            UpdateCourseFields(ref download, ref dto);
            return await _context.SaveChangesAsync();

            void UpdateCourseFields(ref Download download, ref DownloadDTO dto)
            {
                download.CourseId = dto.CourseId;
                download.ModuleId = dto.ModuleId;
                download.Title= dto.Title;
                download.Url = dto.Url;
            } 
        }
    }
}
