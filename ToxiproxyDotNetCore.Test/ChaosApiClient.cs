using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ToxiproxyDotNetCore.Test
{
    public class ChaosApiClient : IApiClient    {
        private readonly HttpClient _httpClient;
        public Uri ApiUri {get;set;}

        public ChaosApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PostmanEcho> GetEchoAsync()
        {
            if(ApiUri == null) {
                throw new NullReferenceException($"ApiUrl value must be set in {nameof(GetEchoAsync)}");
            }

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "ToxiproxyDotNetCore");

            var payload = await _httpClient.GetStringAsync(ApiUri);

            return JsonConvert.DeserializeObject<PostmanEcho>(payload);
        }
    }
}