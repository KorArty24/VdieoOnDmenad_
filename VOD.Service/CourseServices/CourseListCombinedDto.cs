using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.DTOModels.UI;

namespace VOD.Service.CourseServices
{
    public class CourseListCombinedDto
    {
        public CourseListCombinedDto(SortFilterPageOptions sortFilterPageData,
            IEnumerable<CourseWithInstructorAndVideosDTO> courseList)
        {
            SortFilterPageData = sortFilterPageData;
            CourseList = courseList;
        }

        public SortFilterPageOptions SortFilterPageData { get; private set; }
        public IEnumerable<CourseWithInstructorAndVideosDTO> CourseList { get; private set; }
    }
}
