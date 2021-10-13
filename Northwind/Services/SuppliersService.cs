using Northwind.DbContexts;
using Northwind.Models.Entities;
using Northwind.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Services
{
    public class SuppliersService : CrudServiceBase<Supplier>, ISuppliersService
    {
        private readonly INorthwindDbContext _context;
        public SuppliersService(INorthwindDbContext context) : base(context)
        {
            _context = context;
        }        
    }
}
