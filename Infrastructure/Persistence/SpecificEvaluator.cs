using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class SpecificEvaluator
    {
        public static IQueryable<T> GetQuery<T>(this IQueryable<T> query, Specification<T> specification) where T : class
        {
            var result = query;
            if (specification.Criteria != null)
                result = result.Where(specification.Criteria);

            if (specification.Include.Any())
                result = specification.Include.Aggregate(result, (current, include) => current.Include(include));

            if (specification.OrderBy != null)
                result = result.OrderBy(specification.OrderBy);

            if (specification.OrderByDescending != null)
                result = result.OrderByDescending(specification.OrderByDescending);

            if (specification.IsPaginated)
                result = result.Skip(specification.Skip).Take(specification.Take);

            return result;
        }
    }
}
