using System;
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
    public class ToxyProxyTest
    {
        private static Uri _toxiproxyServerUri;
        private static HttpClient _httpClient;
        private static ToxiproxyClient _toxiproxyClient;

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

            // Add an instance of the Uri object with the base toxiproxy server url
            // TODO: move the value to config
            _toxiproxyServerUri = new Uri("http://localhost:8474");

            // Create instance of ToxyproxyClient for testing
            _toxiproxyClient = new ToxiproxyClient(_httpClient, _toxiproxyServerUri);
        }

        ///
        /// Runs after each test
        ///
        [TestCleanup]
        public async Task TearDown()
        { 
            // reset toxiproxy after each test method executes
            await _toxiproxyClient.ResetAsync();
        }

        ///
        /// List existing proxies and get back non-null value
        ///
        [TestMethod]
        public async Task List_Toxiproxy_Proxies_Success() {
            // get the existing proxies
            var proxies = await _toxiproxyClient.ListProxiesAsync();

            // result should not be null
            Assert.IsNotNull(proxies);
        }

        ///
        /// Add a proxy to toxiproxy and get back created proxy
        ///
        [TestMethod]
        public async Task Add_Toxiproxy_Proxy_Success() {
            // add a proxy
            var proxy = new Proxy() {
                Name = "AddToxiproxyProxySuccess",
                Listen = "127.0.0.1:8899",
                Upstream = "postman-echo.com:80",
                Enabled = true
            };

            // get the created proxy
            var createdProxy = await _toxiproxyClient.AddProxyAsync(proxy);

            // clean up after our test by removing the proxy
            await _toxiproxyClient.DeleteProxyAsync(createdProxy.Name);

            // result should not be null
            Assert.IsNotNull(createdProxy);
        }

    }

}