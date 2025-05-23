using Domain.Entities.OrderModules;
using Domain.Exceptions;
using Services.Specification;
using Shared.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService(IUnitOfWork _unitOfWork,
                              IMapper _mapper,
                              IBusketRepository _busketRepo)
                : IOrderService
    {
        public async Task<OrderResultDto> CreateOrderAsync(OrderRequest orderRequest, string Email)
        {
            // Address
            var address = _mapper.Map<OrderAddress>(orderRequest.ShippingAddress);

            // Order Items => busket items 
            var busket = await _busketRepo.GetBusketByIdAsync(orderRequest.BusketId)
                                ?? throw new BusketNotFoundException(orderRequest.BusketId);
            
            var orderItems = new List<OrderItem>();

            foreach (BusketItem item in busket.Items)
            {
                var product = await _unitOfWork.GetRepository<Product, int>().GetAsync(item.Id)
                                    ?? throw new ProductNotFoundExcpetion(item.Id);
                orderItems.Add(CreateOrderItem(item, product));
            }

            // DeliveryMethod
            var deliverMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>()
                                                .GetAsync(orderRequest.DeliveryMethodId)
                                                    ?? throw new DeliveryMethodNotFoundException(orderRequest.DeliveryMethodId);
             
            // SubTotal
            var subTotal = orderItems.Sum(o => o.Price * o.Quantity);

            var existingOrderWithPaymentIntentId = await _unitOfWork.GetRepository<Order, int>()
                                                        .GetAsync(new OrderWithPaymentIntentSpecification(busket.PaymentIntenId));

            if (existingOrderWithPaymentIntentId != null)
                _unitOfWork.GetRepository<Order, int>()
                   .Delete(existingOrderWithPaymentIntentId);

            // Save DB
            var order = new Order(Email, address, orderItems, deliverMethod, subTotal, busket.PaymentIntenId!);

            await _unitOfWork.GetRepository<Order, int>()
                            .AddAsync(order);
            await _unitOfWork.SaveChangesAsync();

            // Map Retuen
            var orderResult = _mapper.Map<OrderResultDto>(order);
            return orderResult;
        }

        private OrderItem CreateOrderItem(BusketItem item, Product product)
                => new OrderItem(new ProductOrderItem(product.Id, product.Name, product.PictureUrl)
                                , item.Quantity, item.Price);

        public async Task<List<DeliveryMethodResultDto>> GetDeliveryMethodResultsAsync()
        {
            var deliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return _mapper.Map<List<DeliveryMethodResultDto>>(deliveryMethods);
        }

        public async Task<OrderResultDto> GetOrderByIdAsync(int id)
        {
            var order = await _unitOfWork.GetRepository<Order, int>()
                                        .GetAsync(new OrderWithSpecifications(id))
                                        ?? throw new OrderNotFoundException(id);
            return _mapper.Map<OrderResultDto>(order);
        }

        public async Task<List<OrderResultDto>> GetOrdersForUserAsync(string email)
        {
            var orders = await _unitOfWork.GetRepository<Order, int>()
                                       .GetAllAsync(new OrderWithSpecifications(email))
                                       ?? throw new OrderNotFoundException(email);
            return _mapper.Map<List<OrderResultDto>>(orders);
        }
    }
}
