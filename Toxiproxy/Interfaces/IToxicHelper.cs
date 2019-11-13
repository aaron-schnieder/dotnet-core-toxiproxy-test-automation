using System.Threading.Tasks;
using System.Collections.Generic;

namespace Toxiproxy
{
    public interface IToxicHelper
    {
        Task<IEnumerable<Toxic>> ListAsync(string proxyName);
        Task<T> GetAsync<T>(string toxicName, string proxyName);
        Task<T> AddAsync<T>(T toxic, string proxyName);
        Task<T> UpdateAsync<T>(T toxic, string toxicNameToUpdate, string proxyName);
        Task DeleteAsync(string toxicName, string proxyName);
    }
}