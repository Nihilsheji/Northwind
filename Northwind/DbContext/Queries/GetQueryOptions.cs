using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Northwind.DbContexts.Queries
{
    public class GetQueryOptions<T> where T : class
    {
        public Expression<Func<T, bool>> Filter { get; set; }
        public int Count { get; set; }
        public int Skip { get; set; }
        public bool Reverse { get; set; }
        public Func<IQueryable<T>, IQueryable<T>> Includes { get; set; }

    }

    public class GetQueryOptions<T, TResult> : GetQueryOptions<T> where T : class
    {
        public Func<IQueryable<T>, IQueryable<TResult>> Select { get; set; }
    }
}
