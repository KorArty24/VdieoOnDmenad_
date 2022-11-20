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
    internal class InstructorService : IInstructorService
    {
        private readonly VODContext _context;
        private readonly IMapper _mapper;

        [TempData] public string Alert { get; set; }
        public InstructorService(VODContext context, IMapper mapper)
        {
            _context = context;
        }

        public Task<InstructorDTO> DeleteAsync(int instructorId)
        {
            throw new NotImplementedException();
        }

        public Task<Instructor> GetInstructorAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<InstructorDTO>> GetInstructorsAsync()
        {
           
            var Instructors = await _context.Instructors.ToListAsync();
            return _mapper.Map<List<InstructorDTO>>(Instructors);
        }
    }
}
