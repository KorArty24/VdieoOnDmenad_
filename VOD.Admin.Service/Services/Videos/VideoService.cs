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

namespace VOD.Admin.Service.Services.Videos
{
    public class VideoService : IVideoService
    {
        private readonly VODContext _context;

        [TempData] public string Alert { get; set; }

        public VideoService(VODContext context)
        {
            _context = context;
        }
        public async Task<int> AddVideosInfoAsync(VideoDTO video)
        {
            if(! await _context.Videos.AnyAsync(x=>x.Title != video.Title &&
            x.Description != x.Description))
            {
                try
                {
                _context.Add(new Video
                {
                    Title = video.Title,
                    Description = video.Description,
                    CourseId = video.CourseId,
                    ModuleId = video.ModuleId,
                    Url = video.Url
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

        public async Task<int> DeleteVideoAsync(int videoId)
        {
             if (await _context.Videos.AnyAsync(c=>c.Id != videoId))
                {
                    Video video = await _context.Videos.SingleAsync(x => x.Id == videoId);
                    _context.Remove(video);
                    return await _context.SaveChangesAsync();
                }
                else
                {
                    Alert = $"No such video";
                    return 0;
                }  
        }

        public async Task<List<VideoDTO>> GetVideosAsync()
        {
            var videos = await _context.Videos.Select(it=> new VideoDTO
            {Id=it.Id,
            Description= it.Description,
            Title = it.Title,
            Course = it.Course.Title,
            Module = it.Module.Title,
            Url = it.Url
            }).ToListAsync();

            return videos;
        }

        public async Task<VideoDTO> GetVideoAsync(int videoId)
        {
             var video = await _context.Videos.Select(
                vid => new VideoDTO {
                Id = vid.Id,
                Title= vid.Title,
                Description = vid.Description,
                Course = vid.Course.Title,
                Module = vid.Module.Title,
                Url = vid.Url
                }).SingleAsync(vid => vid.Id == videoId); 
            return video;
        }

        public async Task<int> UpdateVideosInfoAsync(VideoDTO dto)
        {
            var video = await _context.Videos.SingleOrDefaultAsync(
                x=>x.Id== dto.Id);
            if (video == null)
            {
                throw new ArgumentException("Video not found");
            }
            UpdateCourseFields(ref video, ref dto);
            return await _context.SaveChangesAsync();

            void UpdateCourseFields(ref Video video, ref VideoDTO dto)
            {
                video.Description = dto.Description;
                video.Title = dto.Title;
                video.Module.Title = dto.Module;
                video.Url = dto.Url;
            } 
        }
    }
}
