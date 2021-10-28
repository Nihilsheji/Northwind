using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Models.Response
{
    public class ProductSalesViewBase
    {
        public DateTime? Date { get; set; }
        public int Count { get; set; }
    }
}
