using Microsoft.EntityFrameworkCore;
using MisrInsuranceOrderManagment.Domain.Entities;
using MisrInsuranceOrderManagment.Domain.Specification.Abstraction;
using System.Linq;

namespace MisrInsuranceOrderManagment.Repository.SpecificationEvaluator
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;

            if (spec.Criteria != null)
                query = query.Where(spec.Criteria); // Apply Criteria

            if (spec.OrderBy != null)
                query = query.OrderBy(spec.OrderBy);

            if (spec.OrderByDesc != null)
                query = query.OrderByDescending(spec.OrderByDesc);

            foreach (var include in spec.Includes)
            {
                query = include(query); // Apply Includes
            }

            return query;
        }
    }
}

