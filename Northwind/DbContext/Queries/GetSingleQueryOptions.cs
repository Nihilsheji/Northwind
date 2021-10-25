using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Northwind.DbContexts.Queries
{
    public class GetSingleQueryOptions<T> where T : class 
    {
        public Expression<Func<T, bool>> Filter { get; set; }
        public Func<IQueryable<T>, IQueryable<T>> Includes { get; set; }

        public IQueryable<T> BuildQuery(IQueryable<T> query)
        {
            if (Includes != null)
            {
                query = Includes(query);
            }

            if (Filter != null)
            {
                query = query.Where(Filter);
            }

            return query;
        }
    }

    public class GetSingleQueryOptions<T, TResult> : GetSingleQueryOptions<T> where T : class
    {
        public Func<IQueryable<T>, IQueryable<TResult>> Select { get; set; }

        public new IQueryable<TResult> BuildQuery(IQueryable<T> query)
        {
            query = base.BuildQuery(query);

            IQueryable<TResult> result = null;
            
            if(Select != null)
            {
                result = Select(query);
            }

            return result;
        }
    }
}
