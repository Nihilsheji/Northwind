using Microsoft.EntityFrameworkCore;
using Northwind.DbContexts;
using Northwind.DbContexts.Queries;
using Northwind.Models.Entities;
using Northwind.Services.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Services
{
    public class TerritoriesService : CrudServiceBase<Territory>, ITerritoriesService
    {
        private readonly INorthwindDbContext _context;

        public TerritoriesService(INorthwindDbContext context) : base(context) {
            _context = context;
        }

        public async Task<IEnumerable<Territory>> GetTerritoriesForRegion(int regionId)
        {
            var result = await _context.GetEntities(new GetQueryOptions<Territory>()
            {
                Filter = (Territory t) => t.RegionId == regionId
            });

            return result;
        }

        public async Task<IEnumerable<Territory>> GetTerritoriesForEmployee(int employeeId)
        {
            var demo = await _context.GetEntity(new GetSingleQueryOptions<Employee>()
            {
                Filter = (Employee d) => d.Id == employeeId,
                Includes = (IQueryable<Employee> q) => 
                    q.Include(c => c.Territories)
            });

            return demo.Territories;
        }

        public async Task<bool> AddEmployeeToTerritory(int territoryId, int employeeId) 
        {
            var terr = await _context.GetEntity(new GetSingleQueryOptions<Territory>()
            {
                Filter = (Territory t) => t.Id == territoryId,
                Includes = (IQueryable<Territory> q) => q.Include((Territory t) => t.Employees)
            });

            var empl = await _context.GetEntity<Employee, int>(employeeId);

            if (terr == null || empl == null) return false;

            if (terr.Employees == null)
                terr.Employees = new List<Employee>();

            terr.Employees.Add(empl);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveEmployeeFromTerritory(int territoryId, int employeeId) 
        {
            var terr = await _context.GetEntity(new GetSingleQueryOptions<Territory>()
            {
                Filter = (Territory t) => t.Id == territoryId,
                Includes = (IQueryable<Territory> q) => q.Include((Territory t) => t.Employees)
            });

            var empl = await _context.GetEntity<Employee, int>(employeeId);

            if (terr == null || empl == null) return false;

            if (terr.Employees == null) return false;

            terr.Employees.Remove(empl);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
