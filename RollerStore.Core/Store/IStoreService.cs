using System.Collections.Generic;
using System.Threading.Tasks;

namespace RollerStore.Core.Store
{
    public interface IStoreService
    {
        Task<List<Store>> GetAsync();
        Task<Store> GetByIdAsync(int id);
        Task<Store> UpdateNameAsync(int id, string name);
        Task RemoveAsync(int id);
        Task<Store> AddAsync(Store store);
    }
}
