using Northwind.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Services.Abstractions
{
    public interface IProductsService : ICrudServiceBase<Product>
    {
        Task<IEnumerable<Product>> GetProductsForSupplier(int supplierId);
        Task<IEnumerable<Product>> GetProductsForCategory(int categoryId);
        Task<IEnumerable<Product>> GetProductsForOrder(int orderId);
    }
}
