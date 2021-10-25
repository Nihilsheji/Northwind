using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Models.Enums
{
    public enum OrderStatus
    {
        Undefined,
        InRealization,
        Shipping,
        Delivered,
        Overdue
    }
}
