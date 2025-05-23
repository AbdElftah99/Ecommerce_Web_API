using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class BusketNotFoundException : NotFoundException
    {
        public BusketNotFoundException(string id) : base($"Busket {id} was not found")
        {
        }
    }
}
