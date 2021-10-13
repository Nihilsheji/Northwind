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
    public class TerritoriesService : CrudServiceBase<Territory>, ITerritoriesService
    {
        private readonly INorthwindDbContext _context;

        public TerritoriesService(INorthwindDbContext context) : base(context) { }

        public async Task<IEnumerable<Territory>> GetTerritoriesForRegion(int regionId)
        {
            var result = await _context.GetEntities<Territory>(_context.GetDbSet<Territory>(), new GetQueryOptions<Territory>()
            {
                Filter = (Territory t) => t.RegionId == regionId
            });

            return result;
        }

        public async Task<IEnumerable<Territory>> GetTerritoriesForEmployee(int employeeId)
        {
            var demo = await _context.GetEntity<Employee>(_context.GetDbSet<Employee>(), new GetQueryOptions<Employee>()
            {
                Filter = (Employee d) => d.Id == employeeId,
                Includes = (IQueryable<Employee> q) => 
                    q.Include(c => c.Territories)
            });

            return demo.Territories;
        }
    }
}
