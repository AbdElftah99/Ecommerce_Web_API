using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrderDtos
{
    public class OrderRequest
    {
        public string BusketId { get; set; }
        public OrderAddressDto ShippingAddress { get; set; }
        public int DeliveryMethodId { get; set; }
    }
}
