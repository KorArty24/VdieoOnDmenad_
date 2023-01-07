using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.DTOModels.Admin;
using VOD.Common.Entities;
using VOD.Database.Contexts;

namespace VOD.Admin.Service.Services.Instructors
{
    public class InstructorService : IInstructorService
    {
        private readonly VODContext _context;

        [TempData] public string Alert { get; set; }
        public InstructorService(VODContext context)
        {
            _context = context;
        }

        public async Task<int> DeleteInstructorAsync(int instructorId)
        {
                if (!_context.Courses.Any(c=>c.InstructorId == instructorId))
                {
                    Instructor instructor = await _context.Instructors.SingleAsync(x => x.Id == instructorId);
                    _context.Remove(instructor);
                    return await _context.SaveChangesAsync();
                }
                else
                {
                    Alert = $"Failed to delete Instructor with Id {instructorId}. Remove courses first";
                    return 0;
                }  
        }

        public async Task<InstructorDTO> GetInstructorAsync(int instructorId)
        {
                var instructor = await _context.Instructors.Select(
                    ins => new InstructorDTO {
                    Id = ins.Id,
                    Name= ins.Name,
                    Description = ins.Description,
                    Thumbnail = ins.Thumbnail }).SingleAsync(ins => ins.Id == instructorId);
            return instructor;
        }

        public async Task<List<InstructorDTO>> GetInstructorsAsync()
        {
            var instructors = await _context.Instructors.Select(t=> new InstructorDTO
            {Id=t.Id,
            Description= t.Description,
            Name = t.Name,
            Thumbnail = t.Thumbnail}).ToListAsync();

            return instructors;
        }
        /// <summary>
        /// Update Instructor's info
        /// </summary>
        /// <param name="instructorId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<int> UpdateInstructorsInfoAsync(InstructorDTO dto)
        {
           var instructor = await _context.Instructors.SingleOrDefaultAsync(
                x=>x.Id== dto.Id);
            if (instructor == null)
            {
                throw new ArgumentException("Instructor not found");
            }
            UpdateInstructorFields(ref instructor, ref dto);
            await _context.SaveChangesAsync();
            return 1;

            void UpdateInstructorFields(ref Instructor inst, ref InstructorDTO dto)
            {
                inst.Description = dto.Description;
                inst.Name = dto.Name;
                inst.Thumbnail = dto.Thumbnail;
            }
        }

        public async Task<InstructorDTO> GetOriginal(int instructorid)
        {
            InstructorDTO instr = await _context.Instructors.Select(item => new InstructorDTO
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Thumbnail = item.Thumbnail,
            }).SingleAsync(k=>k.Id == instructorid);

            return instr;
        }

        public async Task<int> AddInstructorsInfoAsync(InstructorDTO instructor)
        {
            if(! _context.Instructors.AnyAsync(x=>x.Name == instructor.Name &&
            x.Description == instructor.Description).Result)
            {
                try
                {
                    var instructorToAdd = new Instructor
                    {
                        Name = instructor.Name,
                        Thumbnail = instructor.Thumbnail,
                        Description = instructor.Description,

                    };  
                _context.Add(instructorToAdd); 
                await _context.SaveChangesAsync(); //starts tracking the added Entity 

                return instructorToAdd.Id;
                } catch
                {
                    throw new DbUpdateException("Error while saving the data");
                }
            } else 
            {
                return 0;
            }
        }

        public async Task<bool> CheckInstructorExists(int instructorId)
        {
            return (await _context.Instructors.AnyAsync(t => t.Id == instructorId);
        }
    }
}
