using Microsoft.EntityFrameworkCore;
using Northwind.DbContexts;
using Northwind.DbContexts.Queries;
using Northwind.Models.Entities;
using Northwind.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Services
{
    public class OrdersService : CrudServiceBase<Order>, IOrdersService
    {
        private readonly INorthwindDbContext _context;

        public OrdersService(INorthwindDbContext context) : base(context) { }
        
        public async Task<IEnumerable<Order>> GetOrdersForEmployee(int employeeId)
        {
            var result = await _context.GetEntities(_context.GetDbSet<Order>(), new GetQueryOptions<Order>()
            {
                Filter = (Order o) => o.EmployeeId == employeeId
            });

            return result;
        }

        public async Task<IEnumerable<Order>> GetOrdersForCustomer(int customerId)
        {
            var result = await _context.GetEntities(_context.GetDbSet<Order>(), new GetQueryOptions<Order>()
            {
                Filter = (Order o) => o.CustomerId == customerId
            });

            return result;
        }

        public async Task<IEnumerable<Order>> GetOrdersForShipper(int shipperId)
        {
            var result = await _context.GetEntities(_context.GetDbSet<Order>(), new GetQueryOptions<Order>()
            {
                Filter = (Order o) => o.ShipViaId == shipperId
            });

            return result;
        }

        public async Task<IEnumerable<Order>> GetOrdersForProduct(int productId)
        {
            var result = await _context.GetEntities(_context.GetDbSet<Product>(), new GetQueryOptions<Product, Order>()
            {
                Filter = (Product o) => o.Id == productId,
                Count = 1,
                Includes = (IQueryable<Product> q) => 
                    q.Include(o => o.OrderDetails).ThenInclude(od => od.Order),
                Select = (IQueryable<Product> q) => q.SelectMany(o => o.OrderDetails.Select(od => od.Order))
            });

            return result;
        }

        public async Task<Order> GetOrderWithDetailsAndProducts(int orderId)
        {
            var result = await _context.GetEntity(_context.GetDbSet<Order>(), new GetQueryOptions<Order>()
            {
                Filter = (Order o) => o.Id == orderId,
                Includes = (IQueryable<Order> q) => 
                    q.Include(o => o.OrderDetails).ThenInclude(od => od.Product)
            });

            return result;
        }

        //get order full
    }
}
