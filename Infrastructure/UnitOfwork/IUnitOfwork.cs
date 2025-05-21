using MisrInsuranceOrderManagment.Domain.Entities;
using MisrInsuranceOrderManagment.Repository.GenericRepository;

namespace MisrInsuranceOrderManagment.Infrastructure.UnitOfwork
{
    public interface IUnitOfwork : IAsyncDisposable
    {
        IGenereicRepository<T> Repository<T>() where T  : BaseEntity;
        Task<int> Save();
    }
}
