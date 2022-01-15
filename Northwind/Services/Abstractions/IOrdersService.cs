using Northwind.Models.Entities;
using Northwind.Models.Request;
using Northwind.Models.Request.Order;
using Northwind.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Services.Abstractions
{
    public interface IOrdersService : ICrudServiceBase<Order, int>
    {
        Task<IEnumerable<OrderListView>> GetOrdersListViewForEmployee(int employeeId);
        Task<IEnumerable<OrderListView>> GetOrdersListViewForCustomer(string customerId);
        Task<IEnumerable<OrderListView>> GetOrdersListViewForShipper(int shipperId);
        Task<IEnumerable<OrderListView>> GetOrdersListViewForProduct(int productId);
        Task<IEnumerable<OrderListView>> GetOrdersListView();
        Task<IEnumerable<OrderListView>> GetFilteredOrdersListView(GetRequest req);
        Task<int> GetFilteredOrdersListViewCount(GetRequest req);
        Task<Order> CreateOrder(CreateOrderRequest req);
        Task<bool> RemoveOrder(int orderId);
    }
}
