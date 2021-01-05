using Beeater.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Beeater.Persistence.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        protected beeaterContext Context { get; set; }

        public RepositoryBase(beeaterContext context)
        {
            Context = context;
        }
        public void Create(IEnumerable<T> entities)
        {
            Context.Set<T>().AddRange(entities);
        }

        public void Delete(IEnumerable<T> entities)
        {
            Context.Set<T>().RemoveRange(entities);
        }

        public IQueryable<T> FindAll()
        {
            return Context.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return Context.Set<T>().Where(expression).AsNoTracking();
        }

        public void Update(IEnumerable<T> entities)
        {
            Context.Set<T>().UpdateRange(entities);
        }
    }
}
