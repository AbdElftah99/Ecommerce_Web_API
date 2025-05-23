using Microsoft.AspNetCore.Authorization;
using Shared.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [Authorize]
    public class OrdersController(IServiceManager _serviceManager) : ApiController
    {
        [HttpPost()] // api/orders
        public async Task<ActionResult<OrderResultDto>> CreateOrder(OrderRequest request)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            return Ok(await _serviceManager.OrderService.CreateOrderAsync(request, email!));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResultDto>> GetOrderById(int id)
            => Ok(await _serviceManager.OrderService.GetOrderByIdAsync(id));

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResultDto>>> GetOrders()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            return Ok(await _serviceManager.OrderService.GetOrdersForUserAsync(email));
        }

        [AllowAnonymous] // Not should be authorized to enter this end point
        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodResultDto>>> GetDeliveryMethods()
            => Ok(await _serviceManager.OrderService.GetDeliveryMethodResultsAsync());
    }
}
