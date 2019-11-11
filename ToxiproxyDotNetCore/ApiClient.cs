using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ToxiproxyDotNetCore.Interfaces;

namespace ToxiproxyDotNetCore
{
    public class ApiClient : IApiClient    {
        private readonly ILogger<ApiClient> _logger;
        private readonly HttpClient _httpClient;

        public ApiClient(HttpClient httpClient, ILogger<ApiClient> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<PostmanEcho> GetEchoAsync()
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "ToxiproxyDotNetCore");

            var stringTask = _httpClient.GetStringAsync("https://postman-echo.com/get?foo1=bar1&foo2=bar2");

            var payload = await stringTask;

            return JsonConvert.DeserializeObject<PostmanEcho>(payload);
        }
    }
}