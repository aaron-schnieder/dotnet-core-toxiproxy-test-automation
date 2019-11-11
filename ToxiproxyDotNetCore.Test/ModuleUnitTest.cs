using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Moq;
using ToxiproxyDotNetCore.Interfaces;

namespace ToxiproxyDotNetCore.Test
{
    [TestClass]
    public class ModuleUnitTest
    {
        private Module _module;

        [TestInitialize]
        public void TestInitialize()
        {}

        [TestMethod]
        public async Task Get_Echo_Success()
        {
            // create a mock instance of IApiClient            
            var mockApiClient = new Mock<IApiClient>();
            mockApiClient.Setup(p => p.GetEchoAsync()).ReturnsAsync(new PostmanEcho());

            _module = new Module(mockApiClient.Object, new Mock<ILogger<Module>>().Object);

            var result = await _module.GetDataAsync();
            
            Assert.IsNotNull(result);
        }
    }
}
