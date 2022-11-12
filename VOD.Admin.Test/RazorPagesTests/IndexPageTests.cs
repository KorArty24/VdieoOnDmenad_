using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Admin.Tests.Base;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Runtime.InteropServices;

namespace VOD.Admin.Tests.RazorPagesTests
{
    [TestFixture]
    public class IndexPageTests : TestBase
    {
        [OneTimeSetUp]
        public void IndexPageControllerTests()
        {
            _factory = new WebApplicationFactory<Startup>();
            _client = _factory.CreateClient();
        }
        //Base test
        [TestCase("/Index")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            //Arrange 
            var client = _factory.CreateClient();
            //Act 
            var response = await client.GetAsync(url);
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }
}
