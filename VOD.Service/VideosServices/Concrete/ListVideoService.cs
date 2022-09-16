using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.DTOModels.UI;
using VOD.Common.Entities;
using VOD.Database.Contexts;
using VOD.Database.QueryObjects;
using VOD.Service.VideosServices.QueryObjects;
using VOD.Service.CommonOptions;
using VOD.Service.VideosServices.Interfaces;

namespace VOD.Service.VideosServices.Concrete
{
    public class ListVideoService : IListVideoService
    {
        private readonly VODContext _context;

        public ListVideoService(VODContext context)
        {
            _context = context;
        }
        public async Task<IQueryable<VideoDTO>> GetVideosForUser 
             (string userId, PageOptions options, int moduleId=0)
        {
            var module = await _context.Modules.SingleAsync(m => m.Id.Equals(moduleId));
            if (module == null) return default(IQueryable<VideoDTO>);
            var userCourse = await _context.UserCourses.
                SingleAsync(uc => uc.UserId.Equals(userId) && (uc.CourseId.Equals(module.CourseId)));
            if (userCourse == null) return default(IQueryable<VideoDTO>);
            var videoQuery = module.Videos.AsQueryable().MapVideoToDTO();
            return videoQuery.Page(options.PageNum-1, options.PageSize);
        }
    }
}
