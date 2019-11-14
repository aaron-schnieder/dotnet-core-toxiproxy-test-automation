using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Toxiproxy
{
    public class ProxyHelper : IProxyHelper
    {
        private readonly HttpClient _httpClient;
        private readonly Uri _toxiproxyServerUri;

        public ProxyHelper(HttpClient httpClient, Uri toxiproxyServerUri){
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _toxiproxyServerUri = toxiproxyServerUri ?? throw new ArgumentNullException(nameof(toxiproxyServerUri));
        }

        public async Task<IEnumerable<Proxy>> ListAsync(){
            // Deserialize the result json into an object
            var result = new List<Proxy>();

            try {
                // Call toxiproxy-server to get the list of proxes
                var response = await _httpClient.GetAsync(_toxiproxyServerUri.AbsoluteUri + "proxies");

                // Ensure we got a success status code in the response
                response.EnsureSuccessStatusCode();

                // Get the content from the response
                var contentString = await response.Content.ReadAsStringAsync();

                // Deserialize the results
                JObject jsonObject = JsonConvert.DeserializeObject<JObject>(contentString);
                foreach(var obj in jsonObject) {
                    result.Add(JsonConvert.DeserializeObject<Proxy>(obj.Value.ToString()));
                }
            }
            catch(Exception exception) {
                // TODO: add logging and handle exception
                return null;
            }

            return result;
        }
        public async Task<Proxy> GetAsync(string proxyName){
            throw new NotImplementedException();
        }
        public async Task<Proxy> AddAsync(Proxy proxy){
            Proxy createdProxy = null;

            try {
                // Serialize the object
                var serializedObject = JsonConvert.SerializeObject(proxy);
                var jsonContent = new StringContent(serializedObject, Encoding.UTF8, "application/json");

                // POST the new proxy
                var result = await _httpClient.PostAsync(_toxiproxyServerUri.AbsoluteUri + "proxies", jsonContent);

                // Ensure the post was successful
                result.EnsureSuccessStatusCode();

                var resultString = await result.Content.ReadAsStringAsync();
                createdProxy = JsonConvert.DeserializeObject<Proxy>(resultString);
            }
            catch(Exception exception) {
                // TODO: add logging and handle exception
                return null;
            }

            return createdProxy;
        }
        public async Task<Proxy> UpdateAsync(string proxyNameToUpdate, Proxy proxy){
            throw new NotImplementedException();
        }
        public async Task DeleteAsync(string proxyName){
            try {
                // DELETE the proxy
                var result = await _httpClient.DeleteAsync($"{_toxiproxyServerUri.AbsoluteUri}proxies/{proxyName}");

                // Ensure the post was successful
                result.EnsureSuccessStatusCode();
            }
            catch(Exception exception) {
                // TODO: add logging and handle exception
            }
        }
    }
}