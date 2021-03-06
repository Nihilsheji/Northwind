using Northwind.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Services.Abstractions
{
    public interface IShippersService : ICrudServiceBase<Shipper, int>
    {
    }
}
