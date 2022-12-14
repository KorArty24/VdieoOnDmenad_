using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Admin.DTO_Models;
using VOD.Common.Entities;
using VOD.Database.Contexts;

namespace VOD.Admin.Service.Services.Instructors
{
    public class InstructorService : IInstructorService
    {
        private readonly VODContext _context;
        private readonly IMapper _mapper;

        [TempData] public string Alert { get; set; }
        public InstructorService(VODContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> DeleteInstructorAsync(int instructorId)
        {
                if (!_context.Courses.Where(c => c.InstructorId == instructorId).Any())
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
                var instructor = await _context.Instructors.SingleAsync(ins => ins.Id == instructorId);
                InstructorDTO instructorDto = _mapper.Map<InstructorDTO>(instructor);
                return instructorDto;
        }

        public async Task<List<InstructorDTO>> GetInstructorsAsync()
        {
           
            var Instructors = await _context.Instructors.ToListAsync();
            return _mapper.Map<List<InstructorDTO>>(Instructors);
        }

        public async Task<Instructor> UpdateInstructorsInfoAsync(int instructorId, InstructorDTO dto)
        {
           InstructorDTO loadedInstructor = GetOriginal(instructorId).Result;
           var instructor = await _context.Instructors.SingleOrDefaultAsync(
                x=>x.Id== dto.Id);
            if (instructor == null)
            {
                throw new ArgumentException("Instructor not found");
            }
            Instructor modified = _mapper.Map<Instructor>(dto);
            await _context.SaveChangesAsync();

            return modified;
        }

        private async Task<InstructorDTO> GetOriginal(int instructorid)
        {
            Instructor instr = await _context.Instructors.SingleOrDefaultAsync(x => x.Id == instructorid);
            return _mapper.Map<InstructorDTO>(instr);
        }
    }
}
