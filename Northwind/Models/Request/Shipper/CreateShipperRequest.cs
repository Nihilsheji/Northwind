﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Models.Request.Shipper
{
    public class CreateShipperRequest
    {
        public string CompanyName { get; set; }
        public string Phone { get; set; }
    }
}
