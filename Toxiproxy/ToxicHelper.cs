using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Toxiproxy
{
    public class ToxicHelper : IToxicHelper
    {
        private readonly HttpClient _httpClient;
        public ToxicHelper(HttpClient httpClient){
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public Task<IEnumerable<Toxic>> ListAsync(string proxyName) {
            throw new NotImplementedException();
        }
        public Task<Toxic> GetAsync(string toxicName, string proxyName) {
            throw new NotImplementedException();
        }
        public Task<Toxic> AddAsync(Toxic toxic, string proxyName) {
            throw new NotImplementedException();
        }
        public Task<Toxic> UpdateAsync(Toxic toxic, string toxicNameToUpdate, string proxyName) {
            throw new NotImplementedException();
        }
        public Task DeleteAsync(string toxicName, string proxyName) {
            throw new NotImplementedException();
        }
    }
}