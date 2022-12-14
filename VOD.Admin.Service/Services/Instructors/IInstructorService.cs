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
        public Task<InstructorDTO> GetInstructorAsync(int instructorId);
        public Task<int> DeleteInstructorAsync(int instructorId);
        public Task<Instructor> UpdateInstructorsInfoAsync(int instructorId, InstructorDTO dto);
    }
}
