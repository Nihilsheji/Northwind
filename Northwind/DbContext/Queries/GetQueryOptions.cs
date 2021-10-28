using Northwind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Northwind.DbContexts.Queries
{
    public class GetQueryOptions<T> where T : class
    {       
        public Expression<Func<T, bool>> Filter { get; set; }
        public int Count { get; set; }
        public int Skip { get; set; }
        public bool Reverse { get; set; }
        public Func<IQueryable<T>, IQueryable<T>> Includes { get; set; }

        public IEnumerable<SortExpression> Sort { get; set; }

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

            if (Reverse)
            {
                query = query.Reverse();
            }

            if (Sort != null)
            {
                var first = true;
                foreach (var exp in Sort)
                {
                    query = exp.BuildExpression(query, ref first) as IQueryable<T>;
                }
            }

            if (Skip != 0)
            {
                query = query.Skip(Skip);
            }

            if (Count != 0)
            {
                query = query.Take(Count);
            }

            return query;
        }
    }

    public class GetQueryOptions<T, TResult> : GetQueryOptions<T> where T : class
    {
        public Func<IQueryable<T>, IQueryable<TResult>> Select { get; set; }

        public new IQueryable<TResult> BuildQuery(IQueryable<T> query)
        {
            query = base.BuildQuery(query);

            IQueryable<TResult> result = null;

            if (Select != null)
            {
                result = Select(query);
            }

            return result;

        }

    }
}
