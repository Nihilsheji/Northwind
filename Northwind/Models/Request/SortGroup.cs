using Northwind.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Models.Request
{
    public class SortGroup
    {
        public IEnumerable<Sort> Sort { get; set; }

        public IEnumerable<SortExpression> GetSortExpressions<T>() where T : IExpAccess<T>
        {
            var result = new List<SortExpression>();

            foreach(var sort in Sort)
            {
                Type t = null;
                var exp = sort.GetExpression<T>(ref t);
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
