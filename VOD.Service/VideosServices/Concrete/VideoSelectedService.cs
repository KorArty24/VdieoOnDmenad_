using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.DTOModels.UI;
using VOD.Database.Contexts;
using VOD.Service.VideosServices.Interfaces;
using VOD.Service.VideosServices.QueryObjects;

namespace VOD.Service.VideosServices.Concrete
{
    public class VideoSelectedService : IVideoSelectedService
    {
        private readonly VODContext _context;

        public VideoSelectedService(VODContext context)
        {
            _context = context;
        }

        public async Task<VideoDTO> SelectedVideoAsync(string userId, int videoId)
        {
            var videoQuery = await _context.Videos.AsNoTracking().SingleAsync(v => v.Id.Equals(videoId));
            if (videoQuery == null) return default;
            var userCource = await _context.UserCourses.AsNoTracking().SingleAsync(c=> c.UserId.Equals(userId) 
            && (c.CourseId.Equals(videoQuery.CourseId)));
            if (userCource == null) return default;
            return VideoDtoSelected.CreateVideoCard(videoQuery);
        }
    }
}
