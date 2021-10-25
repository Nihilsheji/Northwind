using Northwind.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Models.Response
{
    public class OrderListViewBase
    {
        public int Id { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal Total { get; set; }
    }
}
