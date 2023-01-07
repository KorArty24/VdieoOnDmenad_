using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.DTOModels.Admin;
using VOD.Common.Entities;

namespace VOD.Admin.Service.Services.Instructors
{
    public interface IInstructorService
    {
        public Task<List<InstructorDTO>> GetInstructorsAsync();
        public Task<InstructorDTO> GetInstructorAsync(int instructorId);
        public Task<int> DeleteInstructorAsync(int instructorId);
        public Task<int> UpdateInstructorsInfoAsync(InstructorDTO dto);
        public Task<int> AddInstructorsInfoAsync(InstructorDTO instructor);
        public Task<bool> CheckInstructorExists(int instructorId);
    }
}
