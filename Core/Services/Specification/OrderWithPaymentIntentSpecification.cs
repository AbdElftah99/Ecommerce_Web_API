using Domain.Entities.OrderModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specification
{
    public class OrderWithPaymentIntentSpecification : Specification<Order>
    {
        public OrderWithPaymentIntentSpecification(string paymentIntentId) : base(o => o.PaymentIntentId == paymentIntentId)
        {
        }
    }
}
