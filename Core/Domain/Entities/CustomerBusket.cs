using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CustomerBusket
    {
        public string Id { get; set; }
        public IEnumerable<BusketItem> Items { get; set; }

        public string? PaymentIntenId { get; set; }
        public string? ClientSecret { get; set; }
        public int? DeliveryMethodId { get;set; }
        public decimal ShippingPrice { get; set; }
    }
}
