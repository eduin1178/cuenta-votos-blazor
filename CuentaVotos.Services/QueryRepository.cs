
using CuentaVotos.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CuentaVotos.Services
{
    public class QueryRepository<TEntity, TEntityID> where TEntity : class
    {
        private readonly AppDbContext _context;

        public QueryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity?> FindAsync(TEntityID entityID)
        {
            return await _context.Set<TEntity>().FindAsync(entityID);
        }

        public IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _context.Set<TEntity>();
            foreach (var item in includes)
            {
                query.Include(item);
            }

            return query;
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate,
           params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _context.Set<TEntity>();
            foreach (var item in includes)
            {
                query.Include(item);
            }

            return query.Where(predicate);

        }

    }
}
