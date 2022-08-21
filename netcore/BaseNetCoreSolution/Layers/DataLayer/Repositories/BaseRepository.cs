using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CoreLayer.IRepositories;
using CoreLayer.Models.BaseModels;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class BaseRepository<TModel, TKey> : IBaseRepository<TModel, TKey> where TModel : BaseModel<TKey>
    {
        public DbContext _context;
        public DbSet<TModel> _dbSet;
        public BaseRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TModel>();
        }

        public async Task<TModel> GetByIdAsync(TKey id)
        {
            var result = await _dbSet.FindAsync(id);
            if (result == null)
                throw new Exception($"not found {id}");
            return result;
        }

        public async Task<IEnumerable<TModel>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public IQueryable<TModel> WhereQueryable(Expression<Func<TModel, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public void Add(TModel model)
        {
            _context.Entry<TModel>(model).State = EntityState.Added;
        }

        public void Update(TModel model)
        {
            _context.Entry<TModel>(model).State = EntityState.Modified;
        }

        public async Task Remove(TKey id)
        {
            var result = await GetByIdAsync(id);
            result.DeletedTime = DateTime.Now;
            result.IsDeleted = true;
        }
    }
}
