using Microsoft.AspNetCore.Authorization;
using Presentation.Attributes;
using Shared.SpecificationParameters;

namespace Presentation
{
    //[ApiController]
    //[Route("api/[controller]")]
    public class ProductsController(IServiceManager serviceManager) : ApiController
    {
        // Get Methods
        [HttpGet]
        [RedisCache(seconds: 60)]
        public async Task<ActionResult<PaginatedResult<ProductResultDto>>> GetAllProducts([FromQuery]ProductSpecificationParameters specification)
            => Ok(await serviceManager.ProductService.GetAllProductsAsync(specification));

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ProductResultDto>> GetProductById(int id)
            => Ok(await serviceManager.ProductService.GetProductByIdAsync(id));
       
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetAllProductBrands()
            => Ok(await serviceManager.ProductService.GetAllProductBrandsAsync());
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetAllProductTypes()
            => Ok(await serviceManager.ProductService.GetAllProductTypesAsync());
    }
}
