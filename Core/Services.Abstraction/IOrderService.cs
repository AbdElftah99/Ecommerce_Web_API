using Shared.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IOrderService
    {
        // Get order by Id => OrderResultDto
        Task<OrderResultDto> GetOrderByIdAsync(int id);

        // Get Orders For User By Email => List<OrderResultDto>()
        Task<List<OrderResultDto>> GetOrdersForUserAsync(string email);

        // Create Order => OrderResultDto (OrderRequest , string Email)
        Task<OrderResultDto> CreateOrderAsync(OrderRequest orderResultDto, string Email);

        // Get all Delivery MWthods => List<DeliveryMethodResultDto>()
        Task<List<DeliveryMethodResultDto>> GetDeliveryMethodResultsAsync();
    }
}
