using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class BusketProfile : Profile
    {
        public BusketProfile()
        {
            CreateMap<CustomerBusket, BasketDto>().ReverseMap();
            CreateMap<BusketItem, BasketItemDto>().ReverseMap();
        }
    }
}
