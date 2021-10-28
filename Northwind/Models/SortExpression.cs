using Northwind.Utility.TypeExtension;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Northwind.Models
{
    public class SortExpression
    {
        public Type Type { get; set; }
        public object Expression { get; set; }
        public bool Ascending { get; set; }

        public static MethodInfo OrderBy { get; set; } = typeof(Queryable)
            .GetMethodExt("OrderBy", new[] { typeof(IQueryable<>), typeof(Expression<>).MakeGenericType(typeof(Func<,>)) });

        public static MethodInfo OrderByDescending { get; set; } = typeof(Queryable)
            .GetMethodExt("OrderByDescending", new[] { typeof(IQueryable<>), typeof(Expression<>).MakeGenericType(typeof(Func<,>)) });

        public static MethodInfo ThenOrderBy { get; set; } = typeof(Queryable)
            .GetMethodExt("ThenBy", new[] { typeof(IOrderedQueryable<>), typeof(Expression<>).MakeGenericType(typeof(Func<,>)) });

        public static MethodInfo ThenOrderByDescending { get; set; } = typeof(Queryable)
            .GetMethodExt("ThenByDescending", new[] { typeof(IOrderedQueryable<>), typeof(Expression<>).MakeGenericType(typeof(Func<,>)) });

        public object BuildExpression<T>(IQueryable<T> query, ref bool first)
        {
            var type = Type;

            var orderBy = OrderBy.MakeGenericMethod(typeof(T), type);
            var orderByDesc = OrderByDescending.MakeGenericMethod(typeof(T), type);
            var thenOrderBy = ThenOrderBy.MakeGenericMethod(typeof(T), type);
            var thenOrderByDesc = ThenOrderByDescending.MakeGenericMethod(typeof(T), type);

            if (first && Ascending)
            {
                query = orderBy.Invoke(null, new object[] { query, Expression }) as IQueryable<T>;
                first = false;
            }
            else if (first && !Ascending)
            {
                query = orderByDesc.Invoke(null, new object[] { query, Expression }) as IQueryable<T>;
                first = false;
            }
            else if (Ascending)
            {
                query = thenOrderBy.Invoke(null, new object[] { query, Expression }) as IQueryable<T>;
            }
            else
            {
                query = thenOrderByDesc.Invoke(null, new object[] { query, Expression }) as IQueryable<T>;
            }

            return query;
        }
    }
}
