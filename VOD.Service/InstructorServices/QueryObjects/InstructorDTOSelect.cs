using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.DTOModels.UI;
using VOD.Common.Entities;

namespace VOD.Service.InstructorServices.QueryObjects
{
    public static class InstructorDTOSelect
    {
        public static InstructorDTO 
            CreateInstructorCard(Instructor instructor) 
        {
            return new InstructorDTO
            {
                InstructorName = instructor.Name,
                InstructorAvatar = instructor.Thumbnail,
                InstructorDescription = instructor.Description
            };
        }
    }
}
