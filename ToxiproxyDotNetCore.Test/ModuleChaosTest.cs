using System;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace ToxiproxyDotNetCore.Test
{
    [TestClass]
    public class ModuleChaosTest
    {
        private Module _module;
        private static ChaosApiClient _chaosApiClient;

        [ClassInitialize]
        public static void TestFixtureSetup(TestContext context)
        {
            // Executes once for the test class.
            var services = new ServiceCollection();
            services.AddHttpClient();
            var httpClientFactory = services.BuildServiceProvider().GetService<IHttpClientFactory>();
            _chaosApiClient = new ChaosApiClient(httpClientFactory.CreateClient());
        }

        [TestInitialize]
        public void TestInitialize()
        {
            // Clean up the ApiUri before each test
            _chaosApiClient.ApiUri = null;
        }

        [TestMethod]
        public async Task Get_Echo_Success_Test_Api_Endpoint()
        {
            // create a new instance of Module to test
            _module = new Module(_chaosApiClient, new Mock<ILogger<Module>>().Object);

            // set the uri to test
            _chaosApiClient.ApiUri = new Uri("http://localhost:5000/PostmanEcho");

            // test the GetDataAsync method
            var result = await _module.GetDataAsync();

            //"http://127.0.0.1:8080/get?foo1=bar1&foo2=bar2"

            // result should not be null
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Get_Echo_Success_Test_Toxiproxy_Endpoint()
        {
            // create a new instance of Module to test
            _module = new Module(_chaosApiClient, new Mock<ILogger<Module>>().Object);

            // set the uri to test
            _chaosApiClient.ApiUri = new Uri("http://127.0.0.1:8080/get?foo1=bar1&foo2=bar2");

            // test the GetDataAsync method
            var result = await _module.GetDataAsync();

            // result should not be null
            Assert.IsNotNull(result);
        }
    }
}