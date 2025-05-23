using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    public class BasketsController(IServiceManager _serviceManager) : ApiController
    {
        [HttpGet("{basketId}")]
        public async Task<ActionResult<BasketDto>> Get(string basketId)
        {
            var busket = await _serviceManager.BusketService.GetBusketByIdAsync(basketId);
            return Ok(busket);
        }

        [HttpPost]
        public async Task<ActionResult<BasketDto>> Update([FromBody] BasketDto busket)
        {
            var updatedBusket = await _serviceManager.BusketService.UpdateBusketAsync(busket);
            return Ok(updatedBusket);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(string BusketId)
        {
            await _serviceManager.BusketService.DeleteBusketAsync(BusketId);
            return NoContent(); // 204
        }
    }
}
