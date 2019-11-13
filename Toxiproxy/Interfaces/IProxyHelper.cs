using System.Threading.Tasks;
using System.Collections.Generic;

namespace Toxiproxy
{
    public interface IProxyHelper
    {
        Task<IEnumerable<Proxy>> ListAsync();
        Task<Proxy> GetAsync(string proxyName);
        Task<Proxy> AddAsync(Proxy proxy);
        Task<Proxy> UpdateAsync(string proxyNameToUpdate, Proxy proxy);
        Task DeleteAsync(string proxyName);
    }
}