using Northwind.DbContexts;
using Northwind.DbContexts.Queries;
using Northwind.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Northwind.Services
{
    public class CustomersService : CrudServiceBase<Customer>, ICustomersService
    {
        private readonly INorthwindDbContext _context;
    
        public CustomersService(INorthwindDbContext context) : base(context) { }

        public async Task<IEnumerable<Customer>> GetCustomersForDemographic(int demographicId)
        {
            var demo = await _context.GetEntity<Demographic>(_context.GetDbSet<Demographic>(), new GetQueryOptions<Demographic>()
            {
                Filter = (Demographic d) => d.Id == demographicId,
                Includes = (IQueryable<Demographic> q) => 
                    q.Include(d => d.Customers)
            });

            return demo.Customers;
        }
    }
}
