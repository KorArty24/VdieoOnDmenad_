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

namespace VOD.Service.VideosServices.Concrete
{
    public class ListVideoService
    {
        private readonly VODContext _context;

        public ListVideoService(VODContext context)
        {
            _context = context;
        }
        public IQueryable<VideoDTO> GetVideoPage 
            (PageOptions options)
        {
            var videoQuery = _context.Videos.AsNoTracking().MapVideoToDTO();

            return videoQuery.Page(options.PageNum-1, options.PageSize);
        }
    }
}
