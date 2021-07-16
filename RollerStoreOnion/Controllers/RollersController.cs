using Orchestrators = RollerStore.Orschestrators.Roller.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RollerStore.Core.Roller;

namespace RollerStoreOnion.Controllers
{
    [Route("api/RollerStore/v1")]
    [ApiController]
    public class RollersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRollerService _rollerService;

        public RollersController(
            IMapper mapper,
            IRollerService rollerService)
        {
            _mapper = mapper;
            _rollerService = rollerService;
        }

        [HttpGet("Stores/{storeId}/Rollers")]
        public async Task<IActionResult> GetAsync(int storeId)
        {
            var rollers = await _rollerService.GetAsync(storeId);

            return Ok(_mapper.Map<List<Orchestrators.Roller>>(rollers));
        }

        [HttpGet("Rollers/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var roller = await _rollerService.GetByIdAsync(id);

            return Ok(_mapper.Map<Orchestrators.Roller>(roller));
        }

        [HttpPost("Stores/{storeId}/Rollers")]
        public async Task<IActionResult> PostAsync(Orchestrators.CreateRoller roller, int storeId)
        {
            var rollerDomain = _mapper.Map<Roller>(roller);

            rollerDomain.StoreId = storeId;

            var createdRoller = await _rollerService.AddAsync(rollerDomain);

            return Ok(_mapper.Map<Orchestrators.Roller>(createdRoller));
        }

        [HttpDelete("Rollers/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _rollerService.RemoveAsync(id);

            return Ok();
        }

        [HttpPatch("Rollers/{id}")]
        public async Task<IActionResult> UpdatePriceAsync(int rollerId, Orchestrators.UpdateRollerPrice rollerPrice)
        {
            var roller = await _rollerService.UpdatePriceAsync(rollerId, rollerPrice.Price);

            return Ok(_mapper.Map<Orchestrators.Roller>(roller));
        }
    }
}
