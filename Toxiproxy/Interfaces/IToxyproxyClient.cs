using System.Collections.Generic;
using System.Threading.Tasks;

namespace Toxiproxy
{
    public interface IToxiproxyClient 
    {
        Task ResetAsync();

        /* Proxy methods */
        Task<IEnumerable<Proxy>> ListProxiesAsync();
        Task<Proxy> GetProxyAsync(string proxyName);
        Task<Proxy> AddProxyAsync(Proxy proxy);
        Task<Proxy> UpdateProxyAsync(string proxyNameToUpdate, Proxy proxy);
        Task DeleteProxyAsync(string proxyName);
        
        /* Toxic methods */
        Task<IEnumerable<Toxic>> ListToxicsAsync(string proxyName);
        Task<T> GetToxicAsync<T>(string toxicName, string proxyName);
        Task<T> AddToxicAsync<T>(T toxic, string proxyName);
        Task<T> UpdateToxicAsync<T>(T toxic, string toxicNameToUpdate, string proxyName);
        Task DeleteToxicAsync(string toxicName, string proxyName);
    }
}