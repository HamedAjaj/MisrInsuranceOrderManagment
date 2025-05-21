using MisrInsuranceOrderManagment.Domain.Entities;
using MisrInsuranceOrderManagment.Repository.GenericRepository;
using System.Collections;

namespace MisrInsuranceOrderManagment.Infrastructure.UnitOfwork
{
    public class UnitOfWork : IUnitOfwork
    {
        private readonly OrderManagementContext _dbContext;
        private Hashtable _repositories;

        public UnitOfWork(OrderManagementContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Hashtable();
        }

        public IGenereicRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity>(_dbContext);
                _repositories.Add(type, repository);
            }
            return _repositories[type] as IGenereicRepository<TEntity>; // if cast not found then, hashtable will return object 
        }

        public async Task<int> Save() => await _dbContext.SaveChangesAsync();
        public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();
    }
}
