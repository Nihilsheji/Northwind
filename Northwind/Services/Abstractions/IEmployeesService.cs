using Northwind.Models.Entities;
using Northwind.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Services.Abstractions
{
    public interface IEmployeesService : ICrudServiceBase<Employee, int>
    {
        Task<IEnumerable<Employee>> GetEmployeesForTerritory(int territoryId);
        Task<IEnumerable<DictionaryValue<int, string>>> GetDictionary();
        Task<IEnumerable<EmployeeListView>> GetEmployeesListView();
        Task<bool> AddTerritoryToEmployee(int employeeId, int territoryId);
        Task<bool> RemoveTerritoryFromEmployee(int employeeId, int territoryId);
    }
}
