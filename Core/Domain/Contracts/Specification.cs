
using System.Linq.Expressions;

namespace Domain.Contracts
{
    public abstract class Specification<T> where T : class
    {
        public Expression<Func<T, bool>>? Criteria { get; }
        protected Specification(Expression<Func<T, bool>> criteiria)
        {
            Criteria = criteiria;
        }

        public List<Expression<Func<T, object>>> Include { get; private set; } = [];
        public Expression<Func<T, object>>? OrderBy { get; private set; }
        public Expression<Func<T, object>>? OrderByDescending { get; private set; }
        public int Skip { get; private set; }
        public int Take { get; private set; }
        public bool IsPaginated { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
            => Include.Add(includeExpression);

        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
            => OrderBy = orderByExpression;

        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
            => OrderByDescending = orderByDescExpression;

        protected void AddPagination(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPaginated = true;
        }
    }
}
