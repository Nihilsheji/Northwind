using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Models.Request
{
    public class GetRequest
    {
        public int Page { get; set; }
        public int ItemsPerPage { get; set; }

        public FilterGroup Filters { get; set; }
        
        public SortGroup Sorting { get; set; }
    }
}
