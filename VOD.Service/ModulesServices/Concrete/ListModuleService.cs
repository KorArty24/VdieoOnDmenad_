using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.DTOModels.UI;
using VOD.Database.Contexts;
using VOD.Database.QueryObjects;
using VOD.Service.CourseServices;
using VOD.Service.ModulesServices.QueryObjects;

namespace VOD.Service.ModulesServices.Concrete
{
    public class ListModuleService
    {
        private readonly VODContext _context;

        public ListModuleService(VODContext context, IMapper mapper)
        {
            _context = context;
        }
        public IQueryable<ModuleDTO> ModulesPage(SortFilterPageOptions options) {
            var ModuleQuery = _context.Modules.AsNoTracking().MapModuleToDTO();
            return ModuleQuery.Page(options.PageNum - 1, options.PageSize);
        }
    }
}
