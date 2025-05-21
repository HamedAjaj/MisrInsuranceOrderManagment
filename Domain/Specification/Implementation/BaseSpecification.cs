using Microsoft.EntityFrameworkCore.Query;
using MisrInsuranceOrderManagment.Domain.Entities;
using MisrInsuranceOrderManagment.Domain.Specification.Abstraction;
using System.Linq.Expressions;

    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {

   
        public Expression<Func<T, bool>>? Criteria { get; private set; }  
        public List<Func<IQueryable<T>, IQueryable<T>>> Includes { get; private set; } = new();  
        public Expression<Func<T, object>> OrderBy { get; private set; }  
        public Expression<Func<T, object>> OrderByDesc { get; private set; }  
        public int? Skip { get; private set; } 
        public int? Take { get; private set; }  
        public bool IsPaginationEnabled { get; private set; } = false;

        public BaseSpecification() { }

        public BaseSpecification(Expression<Func<T, bool>> criteriaSpecification)
        {
            Criteria = criteriaSpecification;
        }

        public void AddInclude(Func<IQueryable<T>, IQueryable<T>> includeExpression)
        {
            if (includeExpression == null)
                throw new ArgumentNullException(nameof(includeExpression));
            Includes.Add(includeExpression);
        }

    
    public void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        public void AddOrderByDesc(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDesc = orderByDescExpression;
        }

        public void ApplyPagination(int skip, int take)
        {
            if (skip == null || take == null)
                throw new ArgumentNullException("Pagination parameters cannot be null.");

            IsPaginationEnabled = true;
            Skip = skip;
            Take = take;
        }
    }




