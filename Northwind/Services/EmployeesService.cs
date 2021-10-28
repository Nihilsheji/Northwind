using Microsoft.EntityFrameworkCore;
using Northwind.DbContexts;
using Northwind.DbContexts.Queries;
using Northwind.Models.Entities;
using Northwind.Models.Response;
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

        public EmployeesService(INorthwindDbContext context) : base(context) {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesForTerritory(int territoryId)
        {
            var demo = await _context.GetEntity<Territory>(new GetSingleQueryOptions<Territory>()
            {
                Filter = (Territory d) => d.Id == territoryId,
                Includes = (IQueryable<Territory> q) => 
                    q.Include(t => t.Employees)
            });

            return demo.Employees;
        }

        public async Task<IEnumerable<DictionaryValue<int, string>>> GetDictionary()
        {
            return await GetDictionary((Employee e) => new DictionaryValue<int, string>() { Key = e.Id, Value = $"{e.FirstName} {e.LastName}" });
        }

        public async Task<IEnumerable<EmployeeListView>> GetEmployeesListView()
        {
            var employees = await _context.GetEntities(new GetQueryOptions<Employee, EmployeeListView>()
            {
                Select = (IQueryable<Employee> q) => q.Select(e => new EmployeeListView()
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Title = e.Title
                })
            });

            return employees;
        }

        public async Task<bool> AddTerritoryToEmployee(int employeeId, int territoryId)
        {
            var empl = await _context.GetEntity(new GetSingleQueryOptions<Employee>()
            {
                Filter = (Employee e) => e.Id == employeeId,
                Includes = (IQueryable<Employee> q) => q.Include((Employee e) => e.Territories)
            });

            var terr = await _context.GetEntity<Territory, int>(territoryId);

            if (empl == null || terr == null) return false;

            if (empl.Territories == null)
                empl.Territories = new List<Territory>();

            empl.Territories.Add(terr);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveTerritoryFromEmployee(int employeeId, int territoryId)
        {
            var empl = await _context.GetEntity(new GetSingleQueryOptions<Employee>()
            {
                Filter = (Employee e) => e.Id == employeeId,
                Includes = (IQueryable<Employee> q) => q.Include((Employee e) => e.Territories)
            });

            var terr = await _context.GetEntity<Territory, int>(territoryId);

            if (empl == null || terr == null) return false;

            if (empl.Territories == null) return false;

            empl.Territories.Remove(terr);

            await _context.SaveChangesAsync();

            return true;
        }

    }
}
