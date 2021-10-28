using Northwind.Utility;
using System;
using System.Collections.Generic;

namespace Northwind.Models.Request
{
    public class SortGroup
    {
        public IEnumerable<Sort> Sort { get; set; }

        public IEnumerable<SortExpression> GetSortExpressions<T>()
        {
            var result = new List<SortExpression>();

            foreach(var sort in Sort)
            {
                Type t = null;
                var exp = sort.GetExpression<T>();
                t = PropertyAccessor<T>.GetPropertyInfo(sort.Property).PropertyType;

                var sortExp = new SortExpression()
                {
                    Ascending = sort.Ascending,
                    Type = t,
                    Expression = exp
                };

                result.Add(sortExp);
            }

            return result;
        }
    }
}
