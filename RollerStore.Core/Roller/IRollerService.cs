using System.Collections.Generic;
using System.Threading.Tasks;

namespace RollerStore.Core.Roller
{
    public interface IRollerService
    {
        Task<List<Roller>> GetAsync(int storeId);
        Task<Roller> GetByIdAsync(int id);
        Task<Roller> UpdatePriceAsync(int id, double price);
        Task RemoveAsync(int id);
        Task<Roller> AddAsync(Roller roller);
    }
}
