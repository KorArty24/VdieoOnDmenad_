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
using VOD.Service.CourseServices.QueryObjects;

namespace VOD.Service.CourseServices.Concrete
{
    public class ListVideoService
    {
        private readonly VODContext _context;

        public ListVideoService(VODContext context)
        {
            _context = context;
        }
        public IQueryable<VideoDTO> GetVideoPage 
            (SortFilterPageOptions options)
        {
            var videoQuery = _context.Videos.AsNoTracking().MapVideoToDTO();

            return videoQuery.Page(options.PageNum-1, options.PageSize);
        }
    }
}
