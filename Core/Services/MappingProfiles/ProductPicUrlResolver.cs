using Microsoft.Extensions.Configuration;

namespace Services.MappingProfiles
{
    public class ProductPicUrlResolver(IConfiguration config) : IValueResolver<Product, ProductResultDto, string>
    {
        public string Resolve(Product source, ProductResultDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl)) return null!;
            return $"{config["BaseUrl"]}{source.PictureUrl}";
        }
    }
}