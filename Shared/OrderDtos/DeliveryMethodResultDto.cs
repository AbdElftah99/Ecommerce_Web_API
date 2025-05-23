using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrderDtos
{
    public record DeliveryMethodResultDto
    {
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string DeliveryTime { get; set; }
        public double Price { get; set; }
    }
}
