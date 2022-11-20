using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Admin.DTO_Models;
using VOD.Common.Entities;

namespace VOD.Admin.Service.Services.Instructors
{
    public interface IInstructorService
    {
        public Task<List<InstructorDTO>> GetInstructorsAsync();
        public Task<Instructor> GetInstructorAsync(int id);
        public Task<InstructorDTO> DeleteAsync(int instructorId);
    }
}
