using Northwind.Models.Entities;
using Northwind.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northwind.Services.Abstractions
{
    public interface ISuppliersService : ICrudServiceBase<Supplier>
    {
        Task<IEnumerable<DictionaryValue<int, string>>> GetDictionary();
        Task<IEnumerable<SupplierListView>> GetSuppliersListView();
    }
}
