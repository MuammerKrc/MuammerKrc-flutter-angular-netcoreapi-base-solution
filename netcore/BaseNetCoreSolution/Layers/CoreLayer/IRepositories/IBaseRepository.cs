using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CoreLayer.Models.BaseModels;

namespace CoreLayer.IRepositories
{
    public interface IBaseRepository<TModel,Tkey> where TModel:BaseModel<Tkey>
    {
        Task<TModel> GetByIdAsync(Tkey id);
        Task<IEnumerable<TModel>> GetAll();
        IQueryable<TModel> WhereQueryable(Expression<Func<TModel, bool>> predicate);
        void Add(TModel model);
        void Update(TModel model);
        Task Remove(Tkey id);
    }
}
