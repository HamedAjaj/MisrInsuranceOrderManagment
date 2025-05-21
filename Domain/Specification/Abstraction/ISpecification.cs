using Microsoft.EntityFrameworkCore.Query;
using MisrInsuranceOrderManagment.Domain.Entities;
using System.Linq.Expressions;

namespace MisrInsuranceOrderManagment.Domain.Specification.Abstraction
{
    public interface ISpecification<T> where T : BaseEntity
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Func<IQueryable<T>, IQueryable<T>>> Includes { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDesc { get; }
        int? Skip { get; }
        int? Take { get; }
        bool IsPaginationEnabled { get; }

        void AddInclude(Func<IQueryable<T>, IQueryable<T>> includeExpression);
     
        void AddOrderBy(Expression<Func<T, object>> orderByExpression);
        void AddOrderByDesc(Expression<Func<T, object>> orderByDescExpression);
        void ApplyPagination(int skip, int take);
    }

}
