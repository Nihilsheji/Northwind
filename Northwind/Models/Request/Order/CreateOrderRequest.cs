using Northwind.Models.Request.OrderDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Models.Request.Order
{
    public class CreateOrderRequest
    {
        public string CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime? RequiredDate { get; set; }
        public int ShipViaId { get; set; }
        public float Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }

        public IEnumerable<CreateOrderDetailsRequest> OrderDetails { get; set; }
    }
}
