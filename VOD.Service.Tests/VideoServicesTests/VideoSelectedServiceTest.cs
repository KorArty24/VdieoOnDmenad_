using Moq;
using VOD.Common.DTOModels.UI;
using VOD.Service.CourseServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Service.VideosServices.Interfaces;
using VOD.Database.Contexts;
using System.Collections;
using VOD.Service.VideosServices.Concrete;
using VOD.Database.Migrations.DbInitializer;

namespace VOD.Service.Tests.VideoServicesTests
{
    public class VideoSelectedServiceTest : TestBase
    {
        
        [Test]
        [TestCaseSource(typeof(TestValuesClass), nameof(TestValuesClass.UserCourseCases))]
        public void ShouldReturnSelectedVideoDTOForUser(string userId, int videoId)
        {
            //Arrange
            VideoSelectedService service = new VideoSelectedService(context);

            //Act
            var result = service.SelectVideoAsync(userId, videoId).Result;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<VideoDTO>(result);
        }
        
        [Test]
        [TestCaseSource(typeof(TestValuesClass), nameof(TestValuesClass.CaseThatReturnsDefault))]
        public void ShouldReturnDefaultIfCourseNull(string userId, int videoId)
        { 
            //Arrange
            var service = new VideoSelectedService(context);

            //Act
           // var result = service.SelectVideoAsync(userId, videoId).Result;
        }
    }

    public class TestValuesClass
        {
           public static object[] UserCourseCases =
            {
                new object[] { SampleUserData.UserConsts.userOneId, 2}
            };
           public static object[] CaseThatReturnsDefault =
            {
                new object[]{SampleUserData.UserConsts.userOneId, 6},
            };
        }
}
