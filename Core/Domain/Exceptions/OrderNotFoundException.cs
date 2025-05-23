using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class OrderNotFoundException : Exception
    {
        public OrderNotFoundException(int id) : base($"Order with Id {id} not founded")
        {
        }

        public OrderNotFoundException(string email) : base($"Orders for user with email {email} not founded")
        {
        }
    }
}
