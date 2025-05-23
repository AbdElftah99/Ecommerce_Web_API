using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Services
{
    public class ServiceManager(IUnitOfWork _unitOfWork,
                                IBusketRepository _busketRepository ,
                                ICacheRepository _cacheRepository,
                                UserManager<User> _userManager,
                                IOptions<JwtOptions> _jwtOptions,
                                IMapper _mapper,
                                IConfiguration _configuartion) : IServiceManager
    {
        private readonly Lazy<IProductService>  _productService                 = new(() => new ProductService(_unitOfWork, _mapper));
        private readonly Lazy<IBusketService>   _busketService                  = new(() => new BusketService(_busketRepository, _mapper));
        private readonly Lazy<ICacheService>    _cacheService                   = new(() => new CacheService(_cacheRepository));
        private readonly Lazy<IAuthenticationService> _authenticationService    = new(() => new AuthenticationService(_userManager, _jwtOptions, _mapper));
        private readonly Lazy<IOrderService> _orderService                      = new(() => new OrderService(_unitOfWork, _mapper, _busketRepository));
        private readonly Lazy<IPaymentService> _paymentService                  = new(() => new PaymentService(_busketRepository, _unitOfWork, _mapper, _configuartion));
        public IProductService ProductService               => _productService.Value;
        public IBusketService BusketService                 => _busketService.Value;
        public ICacheService CacheService                   => _cacheService.Value;
        public IAuthenticationService AuthenticationService => _authenticationService.Value;
        public IOrderService OrderService                   => _orderService.Value;
        public IPaymentService PaymentService               => _paymentService.Value;
    }
}
