using Voting.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Voting.API.Voting.Infra.DataBase.Context;
using Voting.API.Domain.Entities.Base;

namespace Voting.API.Voting.Infra.DataBase.Repositories.Base
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseDomain, new()
    {
        protected readonly BBBDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected BaseRepository(BBBDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task AdcionarAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            await SaveChangesAsync();
        }

        public virtual void Adcionar(TEntity entity)
        {
            _dbSet.Add(entity);
            SaveChanges();
        }

        public virtual async Task Adcionar(IList<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await SaveChangesAsync();
        }

        public virtual async Task Update(TEntity entity)
        {
            _dbSet.Update(entity);
            await SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<TEntity?> GetById(Guid id)
        {
            return await _dbSet.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public virtual async Task Remover(Guid id)
        {
            var entity = new TEntity { Id = id };
            _dbSet.Remove(entity);
            await SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
