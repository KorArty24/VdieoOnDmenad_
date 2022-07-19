using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.DTOModels.UI;
using VOD.Common.Entities;
using AutoMapper;

namespace VOD.Service.ModulesServices.QueryObjects
{
    public static class ModuleListDTOSelect
    {
        private static IMapper _mapper;
        
        public static void Configure(IMapper mapper) => _mapper = mapper;

        public static IQueryable<ModuleDTO>
            MapCourseToDTO(this IQueryable<Module> modules)
        {
            return modules.Select(module => CreateModuleDTO(module));
        }

        private static ModuleDTO CreateModuleDTO(Module module)
        {
            return  _mapper.Map<ModuleDTO>(module);
        }
    }
}
