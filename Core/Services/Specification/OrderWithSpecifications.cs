using Domain.Entities.OrderModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specification
{
    public class OrderWithSpecifications : Specification<Order>
    {
        public OrderWithSpecifications(int id) : base(o => o.Id == id)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
        }

        public OrderWithSpecifications(string email) : base(o => o.UserEmail == email)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);

            AddOrderByDescending(o => o.OrderDate);
        }

    }
}
