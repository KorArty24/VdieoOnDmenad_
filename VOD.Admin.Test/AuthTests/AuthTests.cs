using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using VOD.Admin.Tests.Base;

namespace VOD.Admin.Tests.AuthTests
{
    [TestFixture]
    public class AuthTests : TestBase
    {
        [OneTimeSetUp]
        public void IndexPageControllerTests()
        {
            _factory = new WebApplicationFactory<Startup>();
            _client = _factory.CreateClient();
        }
        [Test]
        public async Task Get_SecurePageRedirectsAnUnauthenticatedUser()
        {
            //Arrange
            var client = _factory.CreateClient( new WebApplicationFactoryClientOptions {AllowAutoRedirect = false });

            //Act
            var response = await client.GetAsync("/Details");
            
            //Assert
            Assert.AreEqual(HttpStatusCode.Redirect, response.StatusCode);
            Assert.That(response.Headers.Location.OriginalString, Does.StartWith("http://localhost/Identity/Account/Login"));
        }
    }
}
