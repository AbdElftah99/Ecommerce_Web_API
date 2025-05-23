using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrderDtos
{
    public record OrderResultDto
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public OrderAddressDto ShippingAddress { get; set; }
        public string PaymentStatus { get; set; }
        public string DeliveryMethod { get; set; }
        public IEnumerable<OrderItemDto> OrderItems { get; set; } = [];
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public string PaymentIntentId { get; set; } = string.Empty;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
    }
}
