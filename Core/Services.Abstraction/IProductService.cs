using Shared.SpecificationParameters;

namespace Services.Abstraction
{
    public interface IProductService
    {
        // Get All
        Task<PaginatedResult<ProductResultDto>> GetAllProductsAsync(ProductSpecificationParameters specification);

        // Get Product
        Task<ProductResultDto> GetProductByIdAsync(int id);

        // Get All Types
        Task<IEnumerable<TypeResultDto>> GetAllProductTypesAsync();

        // Get All Brands
        Task<IEnumerable<BrandResultDto>> GetAllProductBrandsAsync();
    }
}
