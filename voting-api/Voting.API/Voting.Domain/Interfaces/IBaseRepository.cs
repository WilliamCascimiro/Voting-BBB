using System.Linq.Expressions;
using Voting.API.Domain.Entities.Base;

namespace Voting.Domain.Interfaces
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : BaseDomain
    {
        void Adcionar(TEntity entity);
        Task AdcionarAsync(TEntity entity);
        Task Adcionar(IList<TEntity> entities);
        Task<TEntity> GetById(Guid id);
        Task<List<TEntity>> GetAll();
        Task Update(TEntity entity);
        Task Remover(Guid id);
        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChangesAsync();
        int SaveChanges();
    }
}
