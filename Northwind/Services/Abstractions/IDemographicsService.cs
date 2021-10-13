using Northwind.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Services.Abstractions
{
    public interface IDemographicsService : ICrudServiceBase<Demographic>
    {
        Task<IEnumerable<Demographic>> GetCustomersForDemographic(int demographicId);
    }
}
