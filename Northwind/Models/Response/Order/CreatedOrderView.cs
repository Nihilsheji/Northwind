using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Models.Response
{
    public class CreatedOrderView : OrderView
    {
        public IEnumerable<OrderDetailsView> OrderDetails { get; set; }
    }
}
