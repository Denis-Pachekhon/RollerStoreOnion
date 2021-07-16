using RollerStore.Core.Exeptions;
using RollerStore.Core.Roller;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RollerStore.Orschestrators.Roller
{
    public class RollerService : IRollerService
    {
        private readonly IRollerRepository _rollerRepository;

        public RollerService(IRollerRepository rollerRepository)
        {
            _rollerRepository = rollerRepository;
        }
        public async Task<Core.Roller.Roller> AddAsync(Core.Roller.Roller roller)
        {
            var rollerEntity = await _rollerRepository.AddAsync(roller);

            return rollerEntity;
        }

        public async Task<List<Core.Roller.Roller>> GetAsync(int storeId)
        {
            var rollerEntities = await _rollerRepository.GetAsync(storeId);

            return rollerEntities;
        }

        public async Task<Core.Roller.Roller> GetByIdAsync(int id)
        {
            if (id <= 0 || IsDeleted(id))
            {
                throw new ValidationException("Bad id");
            }

            var rollerEntiry = await _rollerRepository.GetByIdAsync(id);

            return rollerEntiry;
        }

        public async Task RemoveAsync(int id)
        {
            if (IsDeleted(id))
            {
                throw new ValidationException("Bad id");
            }

            var rollerEntity = await _rollerRepository.GetByIdAsync(id);

            if (rollerEntity == null)
            {
                throw new ValidationException($"Roller not found, rollerId = {id}");
            }

            await _rollerRepository.RemoveAsync(rollerEntity.Id);
        }

        public async Task<Core.Roller.Roller> UpdatePriceAsync(int id, double price)
        {
            if (IsDeleted(id))
            {
                throw new ValidationException("Bad id");
            }

            var rollerEntity = await _rollerRepository.GetByIdAsync(id);

            if (rollerEntity == null)
            {
                throw new ValidationException($"Roller not found, rollerId = {id}");
            }

            rollerEntity.Price = price;

            await _rollerRepository.UpdatePriceAsync(rollerEntity);

            return rollerEntity;
        }

        private bool IsDeleted(int id)
        {
            return _rollerRepository.IsDeleted(id);
        }
    }
}
