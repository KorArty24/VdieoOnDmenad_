using Moq;
using System.Collections;
using VOD.Common.DTOModels.UI;
using VOD.Service.CommonOptions;
using VOD.Service.CourseServices.Interfaces;
using VOD.Service.VideosServices.Interfaces;
using VOD.UI.Models.MembershipViewModels;

namespace VOD.ControllerTest
{
    public class ActionTests
    {
        private Mock<IListVideoService> _mockListVideoService;

        [SetUp]
        public void Setup()
        {
            _mockListVideoService = new Mock<IListVideoService>();
           
        }

        [Test]
        [TestCaseSource(typeof(pageOptionsAuxilary))]
        public void ShouldReturnView_VideoList4User(int id, PageOptions options)
        {
            //Arrange 
            VideoDTO video = new VideoDTO
            {
                Id = id,
                Title="Video1",
                Description="See the describtion",
                Duration= 45,
                Url = "youtube.com"
            };
            CourseDTO course = new CourseDTO
            {
                CourseId = 1,
                CourseDescription = "Descr",
                CourseTitle = "Course"
            };
            InstructorDTO instructor = new InstructorDTO
            {
                InstructorDescription = "Best instructor, ever",
                InstructorName = "Jon Smith",
            };
            var model = new VideoViewModel
            {
                Course = course,
                Instructor = instructor,
                Video = video,
            };
            
        }

        internal class pageOptionsAuxilary : IEnumerable
        {
            PageOptions options = new PageOptions { PageSize = 10 };

            public IEnumerator GetEnumerator() {

                yield return new object[] { 1, options };

            }
        }
      
    }
}