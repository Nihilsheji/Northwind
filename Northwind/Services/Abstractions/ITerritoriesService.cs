using Northwind.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Services.Abstractions
{
    public interface ITerritoriesService : ICrudServiceBase<Territory, int>
    {
        Task<IEnumerable<Territory>> GetTerritoriesForRegion(int regionId);
        Task<IEnumerable<Territory>> GetTerritoriesForEmployee(int employeeId);
        Task<bool> AddEmployeeToTerritory(int territoryId, int employeeId);
        Task<bool> RemoveEmployeeFromTerritory(int territoryId, int employeeId);
    }
}
