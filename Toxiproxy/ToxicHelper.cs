using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.Http;

namespace Toxiproxy
{
    public class ToxicHelper : IToxicHelper
    {
        private readonly HttpClient _httpClient;
        public ToxicHelper(HttpClient httpClient){
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public IEnumerable<Toxic> List(string proxyName) {
            throw new NotImplementedException();
        }
        public Toxic Get(string toxicName, string proxyName) {
            throw new NotImplementedException();
        }
        public Toxic Add(Toxic toxic, string proxyName) {
            throw new NotImplementedException();
        }
        public Toxic Update(Toxic toxic, string toxicNameToUpdate, string proxyName) {
            throw new NotImplementedException();
        }
        public void Delete(string toxicName, string proxyName) {
            throw new NotImplementedException();
        }
    }
}