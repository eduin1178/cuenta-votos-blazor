using CuentaVotos.Entiies.Shared;
using System.Linq.Expressions;

namespace CuentaVotos.Repository
{

    public interface IBaseRepository<TEntity>
    {
        void Create(TEntity entity);
        void CreateMany(IEnumerable<TEntity> entities);

        void Update(TEntity entity);
        void UpdateMany(List<TEntity> predicate);
        void Delete(TEntity entity);
        void DeleteMany(Expression<Func<TEntity, bool>> predicate);

        Task<ResultBase> SaveAsync();

    }
    public interface IQueryRepository<TEntity, TEntityID>
    {
        IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includes);
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity?> FindAsync(TEntityID entityID);
    }

}
