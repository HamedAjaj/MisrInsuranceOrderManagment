using MisrInsuranceOrderManagment.Domain.Entities;
using MisrInsuranceOrderManagment.Domain.Specification.Abstraction;

namespace MisrInsuranceOrderManagment.Repository.GenericRepository
{
    public interface IGenereicRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdWithSpecificationAsync(ISpecification<T> spec);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
