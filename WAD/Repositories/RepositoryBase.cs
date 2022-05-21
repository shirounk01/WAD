using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WAD.Models;
using WAD.Repositories.Interfaces;

namespace WAD.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected BookNGoContext Context { get; set; }

        public RepositoryBase(BookNGoContext locationContext)
        {
            this.Context = locationContext;
        }

        public IQueryable<T> FindAll()
        {
            return this.Context.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.Context.Set<T>().Where(expression).AsNoTracking();
        }

        public void Create(T entity)
        {
            this.Context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            this.Context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this.Context.Set<T>().Remove(entity);
        }
    }
}
