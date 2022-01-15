using Northwind.Models.Entities;
using Northwind.Models.Request.OrderDetails;
using Northwind.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Services.Abstractions
{
    public interface IOrderDetailsService : ICrudServiceBase<OrderDetails, int>
    {
        Task<bool> RemoveOrderDetails(RemoveOrderDetailsRequest req);
        Task<OrderDetails> UpdateOrderDetails(UpdateOrderDetailsRequest req);
        Task<OrderDetails> CreateOrderDetails(CreateOrderDetailsRequest req);
        Task<IEnumerable<OrderDetailsListView>> GetOrderDetailsListViewForOrder(int orderId);
    }
}
