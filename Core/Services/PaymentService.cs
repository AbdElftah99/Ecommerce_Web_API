using Domain.Entities.OrderModules;
using Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = Domain.Entities.Product;

namespace Services
{
    public class PaymentService(IBusketRepository   _busketRepo,
                                IUnitOfWork         _unitOfWork,
                                IMapper             _mapper,
                                IConfiguration      _configuration) 
                                            : IPaymentService
    {
        public async Task<BasketDto> CreateOrUpdatePaymentIntent(string busketId)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            // Get Busket
            var busket = await _busketRepo.GetBusketByIdAsync(busketId)
                                ?? throw new BusketNotFoundException(busketId);

            // Adjust the busket Items Prices 
            foreach (var item in busket.Items)
            {
                var product = _unitOfWork.GetRepository<Product, int>().GetAsync(item.Id)
                                            ?? throw new ProductNotFoundExcpetion(item.Id);
            }

            // Check If Busket Deliver Method Exist
            if (!busket.DeliveryMethodId.HasValue) throw new BadRequestExcpetion("Delivery method must be selectes");

            var deliverMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>()
                .GetAsync(busket.DeliveryMethodId.Value)
                ?? throw new DeliveryMethodNotFoundException(busket.DeliveryMethodId.Value);
            
            // Update Shipping price in busket
            busket.ShippingPrice = deliverMethod.Price;

            long amount = (long)(busket.Items.Sum(i => i.Price * i.Quantity) + deliverMethod.Price);


            var serivce = new PaymentIntentService();

            if (string.IsNullOrWhiteSpace(busket.PaymentIntenId))
            {
                // Create new

                var createOptions = new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency = "USD",
                    PaymentMethodTypes = ["card"]
                };
                var paymentIntent = await serivce.CreateAsync(createOptions);
                busket.PaymentIntenId = paymentIntent.Id;
                busket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var updateOptions = new PaymentIntentUpdateOptions
                {
                    Amount = amount,
                    Currency = "USD",
                };

                var paymentIntent = await serivce.UpdateAsync(busket.PaymentIntenId, updateOptions);
            }

            await _busketRepo.UpdateBusketAsync(busket);

            return _mapper.Map<BasketDto>(busket);

        }
    }
}
