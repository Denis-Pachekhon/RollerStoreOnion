using RollerStore.Core.Exeptions;
using RollerStore.Core.Store;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RollerStore.Orschestrators.Store
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;

        public StoreService(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }
        public async Task<Core.Store.Store> AddAsync(Core.Store.Store store)
        {
            var storeEntity = await _storeRepository.AddAsync(store);

            return storeEntity;
        }

        public async Task<List<Core.Store.Store>> GetAsync()
        {
            var storeEntities = await _storeRepository.GetAsync();

            return storeEntities;
        }

        public async Task<Core.Store.Store> GetByIdAsync(int id)
        {
            if (id <= 0 || IsDeleted(id))
            {
                throw new ValidationException("Bad id");
            }

            var storeEntiry = await _storeRepository.GetByIdAsync(id);

            return storeEntiry;
        }

        public async Task RemoveAsync(int id)
        {
            if (IsDeleted(id))
            {
                throw new ValidationException("Bad id");
            }

            var storeEntity = await _storeRepository.GetByIdAsync(id);

            if (storeEntity == null)
            {
                throw new ValidationException($"Store not found, storeId = {id}");
            }

            await _storeRepository.RemoveAsync(storeEntity.Id);
        }

        public async Task<Core.Store.Store> UpdateNameAsync(int id, string name)
        {
            if (IsDeleted(id))
            {
                throw new ValidationException("Bad id");
            }

            var storeEntity = await _storeRepository.GetByIdAsync(id);

            if (storeEntity == null)
            {
                throw new ValidationException($"Store not found, storeId = {id}");
            }

            if (name == null)
            {
                throw new ValidationException("Invalid store name");
            }

            storeEntity.Name = name;

            await _storeRepository.UpdateNameAsync(storeEntity);

            return storeEntity;
        }

        private bool IsDeleted(int id)
        {
            return _storeRepository.IsDeleted(id);
        }
    }
}
