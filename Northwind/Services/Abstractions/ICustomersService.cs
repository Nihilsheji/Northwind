using Northwind.Models.Entities;
using Northwind.Models.Request.Customer;
using Northwind.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northwind.Services.Abstractions
{
    public interface ICustomersService : ICrudServiceBase<Customer>
    {
        Task<IEnumerable<Customer>> GetCustomersForDemographic(int demographicId);
        Task<IEnumerable<CustomerListView>> GetCustomersListViewForDemographic(int demographicId);
        Task<IEnumerable<DictionaryValue<string, string>>> GetDictionary();
        Task<IEnumerable<CustomerListView>> GetCustomersListView();
        Task<Customer> CreateCustomer(CreateCustomerRequest cust);
        Task<bool> AddDemographicToCustomer(string customerId, int demographicId);
        Task<bool> RemoveDemographicFromCustomer(string customerId, int demographicId);
    }
}