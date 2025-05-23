
using Shared.SpecificationParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specification
{
    public class ProductCountSpecification : Specification<Product>
    {
        public ProductCountSpecification(ProductSpecificationParameters specs)
            : base
            (P => (!specs.BrandId.HasValue || P.BrandId == specs.BrandId)
                    && (!specs.TypeId.HasValue || P.TypeId == specs.TypeId)
                    && (string.IsNullOrWhiteSpace(specs.Search) || P.Description.ToLower().Trim().Contains(specs.Search))
                    && (string.IsNullOrWhiteSpace(specs.Search) || P.Name.ToLower().Trim().Contains(specs.Search)))
        {

        }
    }
}
