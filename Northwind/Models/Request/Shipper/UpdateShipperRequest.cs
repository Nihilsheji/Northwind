using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Models.Request.Shipper
{
    public class UpdateShipperRequest
    {
        public string CompanyName { get; set; }
        public string Phone { get; set; }
    }
}
