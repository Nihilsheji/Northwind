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
                    var type = exp.Type;
                    var funcType = typeof(Func<,>);
                    var expression = typeof(Expression<>).MakeGenericType(funcType);

                    var orderBy = typeof(Queryable).GetMethodExt("OrderBy", new[] { typeof(IQueryable<>), expression })
                        .MakeGenericMethod(typeof(T), type);
                    var orderByDesc = typeof(Queryable).GetMethodExt("OrderByDescending", new[] { typeof(IQueryable<>), expression })
                        .MakeGenericMethod(typeof(T), type);
                    var thenOrderBy = typeof(Queryable).GetMethodExt("ThenBy", new[] { typeof(IOrderedQueryable<>), expression })
                        .MakeGenericMethod(typeof(T), type);
                    var thenOrderByDesc = typeof(Queryable).GetMethodExt("ThenByDescending", new[] { typeof(IOrderedQueryable<>), expression })
                        .MakeGenericMethod(typeof(T), type);

                    if (first && exp.Ascending)
                    {
                        query = orderBy.Invoke(null, new object[] { query, exp.Expression }) as IQueryable<T>;
                        first = false;
                    }
                    else if (first && !exp.Ascending)
                    {
                        query = orderByDesc.Invoke(null, new object[] { query, exp.Expression }) as IQueryable<T>;
                        first = false;
                    }
                    else if (exp.Ascending)
                    {
                        query = thenOrderBy.Invoke(null, new object[] { query, exp.Expression }) as IQueryable<T>;
                    }
                    else
                    {
                        query = thenOrderByDesc.Invoke(null, new object[] { query, exp.Expression }) as IQueryable<T>;
                    }
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

    }
}
