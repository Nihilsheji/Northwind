using Northwind.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Services.Abstractions
{
    public interface IOrdersService : ICrudServiceBase<Order>
    {
        Task<IEnumerable<Order>> GetOrdersForEmployee(int employeeId);
        Task<IEnumerable<Order>> GetOrdersForCustomer(int customerId);
        Task<IEnumerable<Order>> GetOrdersForShipper(int shipperId);
        Task<IEnumerable<Order>> GetOrdersForProduct(int productId);
        Task<Order> GetOrderWithDetailsAndProducts(int orderId);
    }
}
