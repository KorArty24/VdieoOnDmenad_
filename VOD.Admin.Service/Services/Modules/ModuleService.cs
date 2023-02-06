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

namespace VOD.Admin.Service.Services.Modules
{
    public class ModuleService : IModuleService
    {
        private readonly VODContext _context;

        [TempData] public string Alert { get; set; }

        public ModuleService(VODContext context)
        {
            _context = context;
        }
        public async Task<int> AddModulesInfoAsync(ModuleDTO module)
        {
            if(await _context.Modules.AnyAsync(x=>x.Title != module.Title &&
            x.CourseId != module.CourseId))
            {
                try
                {
                _context.Add(new Module
                {
                    Title = module.Title,
                    CourseId = module.CourseId,
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

        public async Task<int> DeleteModuleAsync(int moduleId)
        {
             if (!_context.Modules.Any(c=>c.Id == moduleId))
                {
                    Module module = await _context.Modules.SingleAsync(x => x.Id == moduleId);
                    _context.Remove(module);
                    return await _context.SaveChangesAsync();
                }
                else
                {
                    Alert = $"No such module";
                    return 0;
                }  
        }

        public async Task<List<ModuleDTO>> GetModulesAsync()
        {
            var modules = await _context.Modules.Select(it=> new ModuleDTO
            {
                Id=it.Id,
                Title = it.Title,
                Course= it.Course.Title,
            }).ToListAsync();

            return modules;
        }

        public async Task<ModuleDTO> GetModuleAsync(int moduleId)
        {
             var module = await _context.Modules.Select(
                it => new ModuleDTO {
                Id=it.Id,
                Title = it.Title,
                Course= it.Course.Title,
                }).SingleAsync(it => it.Id == moduleId); 
            return module;
        }

        public async Task<int> UpdateModulesInfoAsync(ModuleDTO dto)
        {
            var module = await _context.Modules.SingleOrDefaultAsync(
                x=>x.Id== dto.Id);
            if (module == null)
            {
                throw new ArgumentException("Module not found");
            }
            UpdateCourseFields(ref module, ref dto);
            return await _context.SaveChangesAsync();

            void UpdateCourseFields(ref Module module, ref ModuleDTO dto)
            {
                module.Title = dto.Title;
                module.CourseId = dto.CourseId;
            } 
        }
    }
}
