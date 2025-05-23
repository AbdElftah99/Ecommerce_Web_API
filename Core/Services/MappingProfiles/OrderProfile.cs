using Domain.Entities.Identity;
using Domain.Entities.OrderModules;
using Shared.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderAddress, OrderAddressDto>().ReverseMap();

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.Id, op => op.MapFrom(o => o.Product.Id))
                .ForMember(d => d.PictureUrl, op => op.MapFrom(o => o.Product.PictureUrl))
                .ForMember(d => d.Name, op => op.MapFrom(o => o.Product.Name))
                .ReverseMap();

            CreateMap<Order, OrderResultDto>()
                .ForMember(d => d.PaymentStatus, op => op.MapFrom(o => o.PaymentStatus.ToString()))
                .ForMember(d => d.DeliveryMethod, op => op.MapFrom(o => o.DeliveryMethod.ShortName))
                .ForMember(d => d.Total, op => op.MapFrom(o => o.SubTotal + o.DeliveryMethod.Price))
                .ReverseMap();

            CreateMap<DeliveryMethod, DeliveryMethodResultDto>()
                .ReverseMap();
        }
    }
}
