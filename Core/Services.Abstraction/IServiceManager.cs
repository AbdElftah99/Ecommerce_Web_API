using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IServiceManager
    {
        IProductService ProductService{ get; }
        IBusketService BusketService { get; }
        ICacheService CacheService { get; }
        IAuthenticationService AuthenticationService { get; }
        IOrderService OrderService { get; }
        IPaymentService PaymentService { get; }
    }
}
