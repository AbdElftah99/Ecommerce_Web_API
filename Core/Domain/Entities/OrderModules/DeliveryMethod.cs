using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities.OrderModules
{
    public class DeliveryMethod : BaseEntity<int>
    {
        public DeliveryMethod()
        {
            
        }
        public DeliveryMethod(string shortName, string description, string deliveryTime, decimal price)
        {
            ShortName = shortName;
            Description = description;
            DeliveryTime = deliveryTime;
            Price = price;
        }

        public string ShortName { get; set; }
        public string Description { get; set; }
        public string DeliveryTime { get; set; }
        [JsonPropertyName("cost")] // Map backend's `Price` to frontend's `cost`
        public decimal Price { get; set; }
    }
}
