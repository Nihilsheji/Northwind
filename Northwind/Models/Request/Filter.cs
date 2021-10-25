using Northwind.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Models.Request
{
    public class Filter
    {
        public string Property { get; set; }
        public FilterOperator Operator { get; set; }

        public string Value { get; set; }
    }
}
