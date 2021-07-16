using System.Collections.Generic;
using System.Threading.Tasks;

namespace RollerStore.Core.Roller
{
    public interface IRollerRepository
    {
        Task<List<Roller>> GetAsync(int storeId);
        Task<Roller> GetByIdAsync(int id);
        Task<Roller> UpdatePriceAsync(Roller roller);
        Task RemoveAsync(int id);
        Task<Roller> AddAsync(Roller roller);

        bool IsDeleted(int id);
    }
}
