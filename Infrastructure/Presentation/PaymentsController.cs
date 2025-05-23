using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    public class PaymentsController(IServiceManager _serviceManager) : ApiController
    {
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntesnt(string busketId)
        {
            var paymentIntent = await _serviceManager.PaymentService.CreateOrUpdatePaymentIntent(busketId);
            return Ok(paymentIntent);
        }
    }
}
