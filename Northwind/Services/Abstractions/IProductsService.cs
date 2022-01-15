using Northwind.Models.Entities;
using Northwind.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Services.Abstractions
{
    public interface IProductsService : ICrudServiceBase<Product, int>
    {
        Task<IEnumerable<Product>> GetProductsForSupplier(int supplierId);
        Task<IEnumerable<ProductListView>> GetProductsListViewForSupplier(int supplierId);
        Task<IEnumerable<Product>> GetProductsForCategory(int categoryId);
        Task<IEnumerable<ProductListView>> GetProductsListViewForCategory(int categoryId);
        Task<IEnumerable<Product>> GetProductsForOrder(int orderId);
        Task<IEnumerable<DictionaryValue<int, string>>> GetDictionary();
    }
}
