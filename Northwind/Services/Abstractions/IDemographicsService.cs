using Northwind.Models.Entities;
using Northwind.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Services.Abstractions
{
    public interface IDemographicsService : ICrudServiceBase<Demographic, int>
    {
        Task<IEnumerable<Demographic>> GetCustomersForDemographic(string demographicId);

        Task<IEnumerable<DictionaryValue<int, string>>> GetDictionary();

        Task<bool> AddCustomerToDemographic(int demographicId, string customerId);
        Task<bool> RemoveCustomerFromDemographic(int demographicId, string customerId);

    }
}
