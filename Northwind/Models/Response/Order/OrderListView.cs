using Northwind.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Models.Response
{
    public class OrderListView
    {
        public int Id { get; set; }
        public OrderStatus Status { get; set; }
        public decimal Total { get; set; }
    }
}
