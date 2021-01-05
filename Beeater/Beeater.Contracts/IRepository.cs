using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Beeater.Contracts
{
    public interface IRepository<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Create(IEnumerable<T> entity);
        void Update(IEnumerable<T> entity);
        void Delete(IEnumerable<T> entity);
        public IQueryable<T> Include(params Expression<Func<T, object>>[] includeExpressions);
    }
}
