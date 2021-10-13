using Microsoft.EntityFrameworkCore;
using Northwind.DbContexts;
using Northwind.DbContexts.Queries;
using Northwind.Models.Entities;
using Northwind.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Services
{
    public class DemographicsService : CrudServiceBase<Demographic>, IDemographicsService
    {
        private readonly INorthwindDbContext _context;

        public DemographicsService(INorthwindDbContext context) : base(context) { }

        public async Task<IEnumerable<Demographic>> GetCustomersForDemographic(int demographicId)
        {
            var demo = await _context.GetEntity<Customer>(_context.GetDbSet<Customer>(), new GetQueryOptions<Customer>()
            {
                Filter = (Customer d) => d.Id == demographicId,
                Includes = (IQueryable<Customer> q) => 
                    q.Include(c => c.Demographics)
            });

            return demo.Demographics;
        }
    }
}
