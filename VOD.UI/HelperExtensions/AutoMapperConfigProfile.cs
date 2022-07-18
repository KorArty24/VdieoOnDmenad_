using AutoMapper;
using System;
using System.Collections.Generic;
using VOD.Common.DTOModels.UI;
using VOD.Common.Entities;
namespace VOD.UI.HelperExtensions
{
    public class AutoMapperConfigProfile : Profile
    {
        public AutoMapperConfigProfile()
        {
            CreateMap<Module, ModuleDTO>();
        }
    }
}
