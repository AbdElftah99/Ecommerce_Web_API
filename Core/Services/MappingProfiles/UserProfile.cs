using Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AddressDto, Address>().ReverseMap();
            CreateMap<CreateAddressDto, Address>().ReverseMap();
            CreateMap<UpdateAddressDto, Address>().ReverseMap();
        }
    }
}
