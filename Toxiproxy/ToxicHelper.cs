using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Toxiproxy
{
    public class ToxicHelper : IToxicHelper
    {
        private readonly HttpClient _httpClient;
        private readonly Uri _toxiproxyServerUri;
        public ToxicHelper(HttpClient httpClient, Uri toxiproxyServerUri){
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _toxiproxyServerUri = toxiproxyServerUri ?? throw new ArgumentNullException(nameof(toxiproxyServerUri));
        }

        public async Task<IEnumerable<Toxic>> ListAsync(string proxyName) {
            throw new NotImplementedException();
        }
        public async Task<T> GetAsync<T>(string toxicName, string proxyName) {
            throw new NotImplementedException();
        }
        public async Task<T> AddAsync<T>(T toxic, string proxyName) {
            T createdToxic;

            try {
                // Serialize the object
                var serializedObject = JsonConvert.SerializeObject(toxic);
                var jsonContent = new StringContent(serializedObject, Encoding.UTF8, "application/json");

                // POST the new proxy
                var result = await _httpClient.PostAsync($"{_toxiproxyServerUri.AbsoluteUri}proxies/{proxyName}/toxics", jsonContent);

                // Ensure the post was successful
                result.EnsureSuccessStatusCode();

                var resultString = await result.Content.ReadAsStringAsync();
                createdToxic = JsonConvert.DeserializeObject<T>(resultString);
            }
            catch(Exception exception) {
                // TODO: add logging and handle exception
                throw;
            }

            return createdToxic;
        }
        public async Task<T> UpdateAsync<T>(T toxic, string toxicNameToUpdate, string proxyName) {
            throw new NotImplementedException();
        }
        public async Task DeleteAsync(string toxicName, string proxyName) {
            try {
                // DELETE the toxic
                var result = await _httpClient.DeleteAsync($"{_toxiproxyServerUri.AbsoluteUri}proxies/{proxyName}/toxics/{toxicName}");

                // Ensure the post was successful
                result.EnsureSuccessStatusCode();
            }
            catch(Exception exception) {
                // TODO: add logging and handle exception
                throw;
            }
        }
    }
}