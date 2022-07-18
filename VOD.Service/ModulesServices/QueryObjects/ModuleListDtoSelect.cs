using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.DTOModels.UI;
using VOD.Common.Entities;

namespace VOD.Service.ModulesServices.QueryObjects
{
    public static class CourseListDTOSelect
    {
        public static IQueryable<ModuleDTO>
            MapCourseToDTO(this IQueryable<Module> modules)
        {
            return modules.Select(module => new ModuleDTO
            {
                ModuleTitle= module.Title,
                Videos=module.Videos.Select(x=>x.Title).ToList()
            });
        }
    }
}
