using Northwind.DbContexts;
using Northwind.Models.Entities;
using Northwind.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Services
{
    public class ShippersService : CrudServiceBase<Shipper>, IShippersService
    {
        private readonly INorthwindDbContext _context;

        public ShippersService(INorthwindDbContext context) : base(context) {
            _context = context;
        }
    }
}
