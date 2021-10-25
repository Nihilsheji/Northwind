using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Models.Request.OrderDetails
{
    public class RemoveOrderDetailsRequest
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
    }
}
