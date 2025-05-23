using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductResultDto>()
                .ForMember(pr => pr.BrandName, o => o.MapFrom(p => p.ProductBrand.Name))
                .ForMember(pr => pr.TypeName, o => o.MapFrom(p => p.ProductType.Name))
                .ForMember(pr => pr.PictureUrl, o => o.MapFrom<ProductPicUrlResolver>());
            CreateMap<ProductBrand, BrandResultDto>();
            CreateMap<ProductType, TypeResultDto>(); 
        }
    }
}
