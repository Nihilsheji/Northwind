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
    public class EmployeesService : CrudServiceBase<Employee>, IEmployeesService
    {
        private readonly INorthwindDbContext _context;

        public EmployeesService(INorthwindDbContext context) : base(context) { }

        public async Task<IEnumerable<Employee>> GetEmployeesForTerritory(int territoryId)
        {
            var demo = await _context.GetEntity<Territory>(_context.GetDbSet<Territory>(), new GetQueryOptions<Territory>()
            {
                Filter = (Territory d) => d.Id == territoryId,
                Includes = (IQueryable<Territory> q) => 
                    q.Include(t => t.Employees)
            });

            return demo.Employees;
        }
    }
}
