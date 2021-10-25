using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Models
{
    public class SortExpression
    {
        public Type Type { get; set; }
        public object Expression { get; set; }
        public bool Ascending { get; set; }
    }
}
