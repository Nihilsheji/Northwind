using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Models.Response
{
    public class ProductReportView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Total { get; set; }
    }
}
