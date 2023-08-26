using CuentaVotos.Entiies.Shared;
using CuentaVotos.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Services
{
    public class BaseRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Create(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void CreateMany(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }
        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Entry(entity).CurrentValues.SetValues(entity);
        }

        public void UpdateMany(List<TEntity> entities)
        {
            entities.ForEach(x => _context.Entry(x).State = EntityState.Modified);
            entities.ForEach(x => _context.Entry(x).CurrentValues.SetValues(x));
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void DeleteMany(Expression<Func<TEntity, bool>> predicate)
        {
            var range = _context.Set<TEntity>().Where(predicate);
            _context.Set<TEntity>().RemoveRange(range);
        }

        public async Task<ResultBase> SaveAsync()
        {
            var result = new ResultBase();
            try
            {
                int count = 0;
                count = await _context.SaveChangesAsync(); ;

                result.IsSuccess = true;
                result.Code = 1;
                result.Message = "OK";
                result.Count = count;
            }
            catch (Exception ex)
            {

                result.IsSuccess = false;
                result.Message = ex.Message;
                result.Code = 0;
            }

            return result;
        }


    }
}
