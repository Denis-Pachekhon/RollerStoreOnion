using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RollerStore.Core.Roller;
using RollerStore.Data.DB;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RollerStore.Data.Roller
{
    public class RollerRepository : IRollerRepository
    {
        private readonly IMapper _mapper;
        private readonly RollerStoreDbContext _context;

        public RollerRepository(
            IMapper mapper, 
            RollerStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Core.Roller.Roller> AddAsync(Core.Roller.Roller roller)
        {
            var mappedRoller = _mapper.Map<RollerEntity>(roller);

            var result = await _context.Rollers.AddAsync(mappedRoller);

            await _context.SaveChangesAsync();

            return _mapper.Map<Core.Roller.Roller>(result.Entity);
        }

        public async Task<List<Core.Roller.Roller>> GetAsync(int storeId)
        {
            var entities = await _context.Rollers
                .Where(r => r.StoreId == storeId && r.IsDeleted == false)
                .ToListAsync();

            return _mapper.Map<List<Core.Roller.Roller>>(entities);
        }

        public async Task<Core.Roller.Roller> GetByIdAsync(int id)
        {
            var entity = await _context.Rollers.FirstOrDefaultAsync(s => s.Id == id);

            return _mapper.Map<Core.Roller.Roller>(entity);
        }

        public async Task RemoveAsync(int id)
        {
            var entity = await _context.Rollers.FirstOrDefaultAsync(s => s.Id == id);

            entity.IsDeleted = true;

            _context.Rollers.Update(entity);

            await _context.SaveChangesAsync();
        }

        public async Task<Core.Roller.Roller> UpdatePriceAsync(Core.Roller.Roller roller)
        {
            var mappedRoller = _mapper.Map<RollerEntity>(roller);

            var result = _context.Update(mappedRoller);

            await _context.SaveChangesAsync();

            return _mapper.Map<Core.Roller.Roller>(result.Entity);
        }

        public bool IsDeleted(int id)
        {
            return _context.Rollers.Count(s => s.Id == id && s.IsDeleted == true) > 0;
        }
    }
}
