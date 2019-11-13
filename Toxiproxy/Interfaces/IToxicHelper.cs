using System.Threading.Tasks;
using System.Collections.Generic;

namespace Toxiproxy
{
    public interface IToxicHelper
    {
        Task<IEnumerable<Toxic>> ListAsync(string proxyName);
        Task<Toxic> GetAsync(string toxicName, string proxyName);
        Task<Toxic> AddAsync(Toxic toxic, string proxyName);
        Task<Toxic> UpdateAsync(Toxic toxic, string toxicNameToUpdate, string proxyName);
        Task DeleteAsync(string toxicName, string proxyName);
    }
}