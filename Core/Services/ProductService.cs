using Domain.Exceptions;
using Services.Specification;
using Shared.SpecificationParameters;

namespace Services
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<PaginatedResult<ProductResultDto>> GetAllProductsAsync(ProductSpecificationParameters specification)
        {
            var specs = new ProductWithTypesAndBrandsSpecification(specification);
            var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(specs);
            var count = await _unitOfWork.GetRepository<Product, int>().CountAsync(new ProductCountSpecification(specification));
            var productsDtos =_mapper.Map<IEnumerable<ProductResultDto>>(products);
            return new PaginatedResult<ProductResultDto>
                (
                    specification.PageIndex,
                    specification.PageSize,
                    count,
                    productsDtos
                );
        }

        public async Task<ProductResultDto> GetProductByIdAsync(int id)
        {
            var specs = new ProductWithTypesAndBrandsSpecification(id);
            var prouduct = await _unitOfWork.GetRepository<Product, int>().GetAsync(specs);
            return _mapper.Map<ProductResultDto>(prouduct) ?? throw new ProductNotFoundExcpetion(id);
        }

        public async Task<IEnumerable<BrandResultDto>> GetAllProductBrandsAsync()
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<BrandResultDto>>(brands);
        }

        public async Task<IEnumerable<TypeResultDto>> GetAllProductTypesAsync()
        {
            var types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<TypeResultDto>>(types);
        }
    }
}
