using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Toxiproxy;

namespace ToxiproxyDotNetCore.Test
{
    [TestClass]
    public class ModuleChaosTest
    {
        private Module _module;
        private static ChaosApiClient _chaosApiClient;
        private static Uri _toxiproxyServerUri;
        private static HttpClient _httpClient;

        ///
        /// Executes once for the test class to init shared variables
        ///
        [ClassInitialize]
        public static void TestFixtureSetup(TestContext context)
        {
            // Instantiate ServiceCollection
            var services = new ServiceCollection();

            // Add an instance of HttpClient for use across all test methods
            services.AddHttpClient();
            var httpClientFactory = services.BuildServiceProvider().GetService<IHttpClientFactory>();
            _httpClient = httpClientFactory.CreateClient();
            _chaosApiClient = new ChaosApiClient(_httpClient);

            // Add an instance of the Uri object with the base toxiproxy server url
            // TODO: move the value to config
            _toxiproxyServerUri = new Uri("http://localhost:8474");
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

            // set the uri to test as the local web api test method
            _chaosApiClient.ApiUri = new Uri("http://localhost:5000/PostmanEcho");

            // test the GetDataAsync method
            var result = await _module.GetDataAsync();

            // result should not be null
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Get_Echo_Success_Test_Toxiproxy_Endpoint()
        {
            // create a new toxiproxy proxy
            var toxiproxyClient = new ToxiproxyClient(_httpClient, _toxiproxyServerUri);

            // add the toxiproxy proxy we will use for testing
            var proxy = await toxiproxyClient.AddProxyAsync(new Proxy() {
                Name = "GetEchoSuccessTestToxiproxyEndpoint",
                Listen = "127.0.0.1:8888",
                Upstream = "postman-echo.com:80",
                Enabled = true
            });

            PostmanEcho result = null;
            try {
                // set the uri to test as the toxiproxy proxy upstream endpoint
                _chaosApiClient.ApiUri = new Uri($"http://{proxy.Listen}/get?foo1=bar1&foo2=bar2");

                // create a new instance of Module to test
                _module = new Module(_chaosApiClient, new Mock<ILogger<Module>>().Object);

                // test the GetDataAsync method
                result = await _module.GetDataAsync();
            }
            catch(Exception exception) {
                // TODO: add logging
                throw;
            }
            finally {
                // clean up the proxy we created for testing
                await toxiproxyClient.DeleteProxyAsync(proxy.Name);
            }

            // result should not be null
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Get_Echo_Latency_Test_Toxiproxy_Endpoint()
        {
            // var to specify 5 seconds of latency to introduce into the API call
            var latency = 5000;

            // create a new ToxiproxyClient instance
            var toxiproxyClient = new ToxiproxyClient(_httpClient, _toxiproxyServerUri);

            // add the toxiproxy proxy we will use for testing
            var proxy = await toxiproxyClient.AddProxyAsync(new Proxy() {
                Name = "GetEchoLatencyTestToxiproxyEndpoint",
                Listen = "127.0.0.1:8888",
                Upstream = "postman-echo.com:80",
                Enabled = true
            });

            // create a new latency toxic instance
            var newToxic = new LatencyToxic() {
                Name = "GetEchoLatencyTestToxic",
                Type = LatencyToxic.ToxicType,
                Stream = "upstream", // TODO: need to move to enum
                Toxicity = 1
            };
            newToxic.Attributes.Latency = latency;
            newToxic.Attributes.Jitter = 0;

            // add the toxic to the toxiproxy proxy
            var toxic = await toxiproxyClient.AddToxicAsync<LatencyToxic>(newToxic, proxy.Name);

            PostmanEcho result = null;
            var stopWatch = new Stopwatch();
            try {
                // set the uri to test as the toxiproxy proxy upstream endpoint
                _chaosApiClient.ApiUri = new Uri($"http://{proxy.Listen}/get?foo1=bar1&foo2=bar2");

                // create a new instance of Module to test
                _module = new Module(_chaosApiClient, new Mock<ILogger<Module>>().Object);

                // test the GetDataAsync method and track the execution time
                stopWatch.Start();
                result = await _module.GetDataAsync();
                stopWatch.Stop();
                
            }
            catch(Exception exception) {
                // TODO: add logging
                throw;
            }
            finally {
                // clean up the proxy we created for testing
                await toxiproxyClient.DeleteProxyAsync(proxy.Name);
            }

            // result should not be null
            Assert.IsNotNull(result);

            // API call should have taken longer to execute than the latency injected into toxiproxy
            Assert.IsTrue(stopWatch.ElapsedMilliseconds > latency);
        }
    }
}