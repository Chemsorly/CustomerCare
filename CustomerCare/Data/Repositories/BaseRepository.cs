using CustomerCare.Data.Dataproviders;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCare.Data.Repositories
{
    internal abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        protected BaseContext CreateContext() { return new EntityFrameworkContext(); }

        protected BaseRepository() { }

        public abstract void Create(T item);

        public abstract void Delete(int id);

        public abstract T Get(int id);

        public abstract void Update(T item);

        protected void ApplyChanges(object applyTo, object applyFrom)
        {
            var inproperties = applyTo.GetType().GetProperties().Where(t => t.CanWrite);
            foreach (var prop in inproperties)
            {
                var fromvalue = applyFrom.GetType().GetProperty(prop.Name).GetValue(applyFrom);
                if (prop.GetValue(applyTo) != fromvalue)
                    prop.SetValue(applyTo, fromvalue);
            }
        }

        public IQueryable<T> GetAllIncluding(IQueryable<T> queryable, params Expression<Func<T, object>>[] includeProperties)
        {
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include<T, object>(includeProperty);
            }

            return queryable;
        }
    }
}
