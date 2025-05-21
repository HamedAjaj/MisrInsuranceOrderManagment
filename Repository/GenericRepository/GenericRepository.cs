using Microsoft.EntityFrameworkCore;
using MisrInsuranceOrderManagment.Domain.Entities;
using MisrInsuranceOrderManagment.Domain.Specification.Abstraction;
using MisrInsuranceOrderManagment.Infrastructure;
using MisrInsuranceOrderManagment.Repository.SpecificationEvaluator;

namespace MisrInsuranceOrderManagment.Repository.GenericRepository
{
    public class GenericRepository<T> : IGenereicRepository<T> where T :BaseEntity
    {
        private readonly OrderManagementContext _dbContext; 
        public GenericRepository(OrderManagementContext context)
        {
            _dbContext = context;
        }
        public async Task<IEnumerable<T>> GetAllAsync() => await _dbContext.Set<T>().ToListAsync();
        
        public async Task<T> GetByIdAsync(int id) => await _dbContext.Set<T>().FindAsync(id);
        
        public async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }
        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public  async Task<T> GetByIdWithSpecificationAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            if (spec == null)
                throw new ArgumentNullException(nameof(spec), "Specification cannot be null.");
            return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec);
        }

    }
}
