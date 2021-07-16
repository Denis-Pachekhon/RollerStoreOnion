using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RollerStore.Core.Store;
using RollerStore.Data.DB;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RollerStore.Data.Store
{
    public class StoreRepository : IStoreRepository
    {
        private readonly IMapper _mapper;
        private readonly RollerStoreDbContext _context;

        public StoreRepository(
            IMapper mapper,
            RollerStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<Core.Store.Store>> GetAsync()
        {
            var entities = await _context.Stores
                .Where(s => s.IsDeleted == false)
                .ToListAsync();

            return _mapper.Map<List<Core.Store.Store>>(entities);
        }

        public async Task<Core.Store.Store> GetByIdAsync(int id)
        {
            var entity = await _context.Stores.FirstOrDefaultAsync(s => s.Id == id);

            return _mapper.Map<Core.Store.Store>(entity);
        }

        public async Task<Core.Store.Store> UpdateNameAsync(Core.Store.Store store)
        {
            var mappedStore = _mapper.Map<StoreEntity>(store);

            var result = _context.Update(mappedStore);

            await _context.SaveChangesAsync();

            return _mapper.Map<Core.Store.Store>(result.Entity);
        }

        public async Task<Core.Store.Store> AddAsync(Core.Store.Store store)
        {
            var mappedStore = _mapper.Map<StoreEntity>(store);

            var result = await _context.AddAsync(mappedStore);

            await _context.SaveChangesAsync();

            return _mapper.Map<Core.Store.Store>(result.Entity);
        }

        public async Task RemoveAsync(int id)
        {
            var entity = await _context.Stores.FirstOrDefaultAsync(s => s.Id == id);

            foreach (var roller in entity.Rollers)
            {
                roller.IsDeleted = true;
            }

            entity.IsDeleted = true;

            _context.Stores.Update(entity);

            await _context.SaveChangesAsync();
        }

        public bool IsDeleted(int id)
        {
            return _context.Stores.Count(s => s.Id == id && s.IsDeleted == true) > 0;
        }
    }
}
