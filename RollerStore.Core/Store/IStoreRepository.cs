using System.Collections.Generic;
using System.Threading.Tasks;

namespace RollerStore.Core.Store
{
    public interface IStoreRepository
    {
        Task<List<Store>> GetAsync();
        Task<Store> GetByIdAsync(int id);
        Task<Store> UpdateNameAsync(Store store);
        Task RemoveAsync(int id);
        Task<Store> AddAsync(Store store);

        bool IsDeleted(int id);
    }
}
