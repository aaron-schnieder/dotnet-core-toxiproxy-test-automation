using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Toxiproxy
{
    public class ToxiproxyClient : IToxiproxyClient
    {
        private readonly HttpClient _httpClient;
        private readonly IProxyHelper _proxyHelper;
        private readonly IToxicHelper _toxicHelper;
        private readonly Uri _toxiproxyServerUri;

        public ToxiproxyClient(HttpClient httpClient, Uri toxiproxyServerUri, IProxyHelper proxyHelper = null, IToxicHelper toxicHelper = null) {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _toxiproxyServerUri = toxiproxyServerUri ?? throw new ArgumentNullException(nameof(toxiproxyServerUri));
            _proxyHelper = proxyHelper ?? new ProxyHelper(httpClient, toxiproxyServerUri);
            _toxicHelper = toxicHelper ?? new ToxicHelper(httpClient);
        }

        ///
        /// Resets all proxies and removes all toxics
        ///
        public async Task ResetAsync(){
            try {
                var result = await _httpClient.PostAsync(_toxiproxyServerUri.AbsoluteUri + "reset", new StringContent(string.Empty));
                result.EnsureSuccessStatusCode();
            }
            catch(Exception exception) {
                // TODO: Add logging
            }

        }

        public async Task<IEnumerable<Proxy>> ListProxiesAsync() {
            return await _proxyHelper.ListAsync();
        }

        public async Task<Proxy> GetProxyAsync(string proxyName) {
            throw new NotImplementedException();
        }

        public async Task<Proxy> AddProxyAsync(Proxy proxy){
            return await _proxyHelper.AddAsync(proxy);
        }

        public async Task<Proxy> UpdateProxyAsync(string proxyNameToUpdate, Proxy proxy){
            throw new NotImplementedException();
        }

        public async Task DeleteProxyAsync(string proxyName) {
            await _proxyHelper.DeleteAsync(proxyName);
        }
    }
}