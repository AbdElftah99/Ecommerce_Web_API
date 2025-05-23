
using Shared.SpecificationParameters;

namespace Services.Specification
{
    public class ProductWithTypesAndBrandsSpecification : Specification<Product>
    {
        public ProductWithTypesAndBrandsSpecification(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
        public ProductWithTypesAndBrandsSpecification(ProductSpecificationParameters specs) 
            : base
            (P => (!specs.BrandId.HasValue  || P.BrandId == specs.BrandId) 
                    && (!specs.TypeId.HasValue || P.TypeId == specs.TypeId)
                    && (string.IsNullOrWhiteSpace(specs.Search) || P.Description.ToLower().Trim().Contains(specs.Search))
                    && (string.IsNullOrWhiteSpace(specs.Search) || P.Name.ToLower().Trim().Contains(specs.Search)))
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

            if (specs.Sort is not null)
            {
                switch (specs.Sort)
                {
                    case ProductSort.PriceAsc:
                        AddOrderBy(p => p.Price);
                        break;
                    case ProductSort.PriceDesc:
                        AddOrderByDescending(p => p.Price);
                        break;
                    case ProductSort.NameAsc:
                        AddOrderBy(p => p.Name);
                        break;
                    case ProductSort.NameDesc:
                        AddOrderByDescending(p => p.Name);
                        break;
                    default:
                        break;
                }
            }

            AddPagination(specs.PageSize * (specs.PageIndex - 1), specs.PageSize);
        }
    }
}
