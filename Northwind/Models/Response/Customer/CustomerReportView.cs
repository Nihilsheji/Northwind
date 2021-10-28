using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Models.Response
{
    public class CustomerReportView
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Total { get; set; }
    }
}
