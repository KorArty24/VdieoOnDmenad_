using System.Collections.Generic;
using VOD.Common.DTOModels.UI;

namespace VOD.UI.Models.MembershipViewModels
{
    public class DashboardViewModel
    {
        public List<List<CourseWithInstructorAndVideosDTO>> Courses { get; set; }
    }
}
