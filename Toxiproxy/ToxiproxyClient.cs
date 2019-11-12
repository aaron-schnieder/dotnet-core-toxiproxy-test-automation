using System;
using System.Net.Http;
using Microsoft.Extensions.Http;

namespace Toxiproxy
{
    public class ToxiproxyClient : IToxiproxyClient
    {
        private readonly HttpClient _httpClient;
        private readonly IProxyHelper _proxyHelper;
        private readonly IToxicHelper _toxicHelper;
        public ToxiproxyClient(HttpClient httpClient, IProxyHelper proxyHelper, IToxicHelper toxicHelper) {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _proxyHelper = proxyHelper ?? new ProxyHelper(httpClient);
            _toxicHelper = toxicHelper ?? new ToxicHelper(httpClient);
        }

        public void Reset(){
            throw new NotImplementedException();
        }
    }
}