using Northwind.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Services.Abstractions
{
    public interface ITerritoriesService : ICrudServiceBase<Territory>
    {
        Task<IEnumerable<Territory>> GetTerritoriesForRegion(int regionId);
        Task<IEnumerable<Territory>> GetTerritoriesForEmployee(int employeeId);
    }
}
