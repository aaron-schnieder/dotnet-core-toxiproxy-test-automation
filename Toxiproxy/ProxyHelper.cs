using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.Http;

namespace Toxiproxy
{
    public class ProxyHelper : IProxyHelper
    {
        private readonly HttpClient _httpClient;

        public ProxyHelper(HttpClient httpClient){
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public IEnumerable<Proxy> List(){
            throw new NotImplementedException();
        }
        public Proxy Get(string proxyName){
            throw new NotImplementedException();
        }
        public Proxy Add(Proxy proxy){
            throw new NotImplementedException();
        }
        public Proxy Update(string proxyNameToUpdate, Proxy proxy){
            throw new NotImplementedException();
        }
        public void Delete(string proxyName){
            throw new NotImplementedException();
        }
    }
}