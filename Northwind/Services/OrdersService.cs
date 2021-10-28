using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Northwind.DbContexts;
using Northwind.DbContexts.Queries;
using Northwind.Models.Entities;
using Northwind.Models.Enums;
using Northwind.Models.Request;
using Northwind.Models.Request.Order;
using Northwind.Models.Response;
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
        private readonly IMapper _mapper;

        public OrdersService(INorthwindDbContext context, IMapper mapper) : base(context) {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<OrderListView>> GetOrdersListViewForEmployee(int employeeId)
        {
            var listBase = await _context.GetEntities(new GetQueryOptions<Order, OrderListViewBase>()
            {
                Filter = (Order o) => o.EmployeeId == employeeId,
                Includes = (IQueryable<Order> q) => q.Include(o => o.OrderDetails),
                Select = (IQueryable<Order> q) => q.Select(o => new OrderListViewBase()
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    RequiredDate = o.RequiredDate,
                    ShippedDate = o.ShippedDate,
                    Total = Convert.ToDouble(o.OrderDetails.Sum(od => od.Quantity * od.UnitPrice))                   
                })
            });


            var result = listBase.Select(x =>
            {
                var view = _mapper.Map<OrderListView>(x);

                var status = OrderStatus.Undefined;

                if (x.ShippedDate != null) status = OrderStatus.Delivered;
                else if (x.RequiredDate < DateTime.Now) status = OrderStatus.Overdue;
                else if (x.OrderDate != null) status = OrderStatus.Shipping;
                else status = OrderStatus.InRealization;

                view.Status = status;

                return view;
            });

            return result;
        }

        public async Task<IEnumerable<OrderListView>> GetOrdersListViewForCustomer(string customerId)
        {
            var listBase = await _context.GetEntities(new GetQueryOptions<Order, OrderListViewBase>()
            {
                Filter = (Order o) => o.CustomerId == customerId,
                Includes = (IQueryable<Order> q) => q.Include(o => o.OrderDetails),
                Select = (IQueryable<Order> q) => q.Select(o => new OrderListViewBase()
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    RequiredDate = o.RequiredDate,
                    ShippedDate = o.ShippedDate,
                    Total = Convert.ToDouble(o.OrderDetails.Sum(od => od.Quantity * od.UnitPrice))
                })
            });


            var result = listBase.Select(x =>
            {
                var view = _mapper.Map<OrderListView>(x);

                var status = OrderStatus.Undefined;

                if (x.ShippedDate != null) status = OrderStatus.Delivered;
                else if (x.RequiredDate < DateTime.Now) status = OrderStatus.Overdue;
                else if (x.OrderDate != null) status = OrderStatus.Shipping;
                else status = OrderStatus.InRealization;

                view.Status = status;

                return view;
            });

            return result;
        }

        public async Task<IEnumerable<OrderListView>> GetOrdersListViewForShipper(int shipperId)
        {
            var listBase = await _context.GetEntities(new GetQueryOptions<Order, OrderListViewBase>()
            {
                Filter = (Order o) => o.ShipViaId == shipperId,
                Includes = (IQueryable<Order> q) => q.Include(o => o.OrderDetails),
                Select = (IQueryable<Order> q) => q.Select(o => new OrderListViewBase()
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    RequiredDate = o.RequiredDate,
                    ShippedDate = o.ShippedDate,
                    Total = Convert.ToDouble(o.OrderDetails.Sum(od => od.Quantity * od.UnitPrice))
                })
            });


            var result = listBase.Select(x =>
            {
                var view = _mapper.Map<OrderListView>(x);

                var status = OrderStatus.Undefined;

                if (x.ShippedDate != null) status = OrderStatus.Delivered;
                else if (x.RequiredDate < DateTime.Now) status = OrderStatus.Overdue;
                else if (x.OrderDate != null) status = OrderStatus.Shipping;
                else status = OrderStatus.InRealization;

                view.Status = status;

                return view;
            });

            return result;
        }

        public async Task<IEnumerable<OrderListView>> GetOrdersListViewForProduct(int productId)
        {
            var listBase = await _context.GetEntities(new GetQueryOptions<Product, OrderListViewBase>()
            {
                Filter = (Product o) => o.Id == productId,
                Count = 1,
                Includes = (IQueryable<Product> q) =>
                    q.Include(o => o.OrderDetails).ThenInclude(od => od.Order),
                Select = (IQueryable<Product> q) => 
                    q.SelectMany(o => o.OrderDetails.Select(od => od.Order))
                    .Select(o => new OrderListViewBase
                    {
                        Id = o.Id,
                        OrderDate = o.OrderDate,
                        RequiredDate = o.RequiredDate,
                        ShippedDate = o.ShippedDate,
                        Total = Convert.ToDouble(o.OrderDetails.Sum(od => od.Quantity * od.UnitPrice))
                    })
            });


            var result = listBase.Select(x =>
            {
                var view = _mapper.Map<OrderListView>(x);

                var status = OrderStatus.Undefined;

                if (x.ShippedDate != null) status = OrderStatus.Delivered;
                else if (x.RequiredDate < DateTime.Now) status = OrderStatus.Overdue;
                else if (x.OrderDate != null) status = OrderStatus.Shipping;
                else status = OrderStatus.InRealization;

                view.Status = status;

                return view;
            });

            return result;
        }

        public async Task<IEnumerable<OrderListView>> GetOrdersListView()
        {
            var listBase = await _context.GetEntities(new GetQueryOptions<Order, OrderListViewBase>()
            {
                Includes = (IQueryable<Order> q) => q.Include(o => o.OrderDetails).ThenInclude(od => od.Product),
                Select = (IQueryable<Order> q) => q.Select(o => new OrderListViewBase()
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    RequiredDate = o.RequiredDate,
                    ShippedDate = o.ShippedDate,
                    Total = Convert.ToDouble(o.OrderDetails.Sum(od => od.UnitPrice * od.Quantity))
                }),
            });

            var result = listBase.Select(x => 
            {
                var view = _mapper.Map<OrderListView>(x);

                var status = OrderStatus.Undefined;

                if (x.ShippedDate != null) status = OrderStatus.Delivered;
                else if (x.RequiredDate < DateTime.Now) status = OrderStatus.Overdue;
                else if (x.OrderDate != null) status = OrderStatus.Shipping;
                else status = OrderStatus.InRealization;

                view.Status = status;

                return view;
            });

            return result;
        }

        public async Task<IEnumerable<OrderListView>> GetFilteredOrdersListView(GetRequest req)
        {
            var listBase = await _context.GetEntities(new GetQueryOptions<Order, OrderListViewBase>()
            {
                Includes = (IQueryable<Order> q) => q.Include(o => o.OrderDetails).ThenInclude(od => od.Product),
                Skip = req.ItemsPerPage * req.Page,
                Count = req.ItemsPerPage,
                Sort = req.Sorting?.GetSortExpressions<Order>(),
                Filter = req.Filters?.GetExpression<Order>(),
                Select = (IQueryable<Order> q) => q.Select(o => new OrderListViewBase()
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    RequiredDate = o.RequiredDate,
                    ShippedDate = o.ShippedDate,
                    Total = Convert.ToDouble(o.OrderDetails.Sum(od => od.UnitPrice * od.Quantity))
                }),
            });

            var result = listBase.Select(x =>
            {
                var view = _mapper.Map<OrderListView>(x);

                var status = OrderStatus.Undefined;

                if (x.ShippedDate != null) status = OrderStatus.Delivered;
                else if (x.RequiredDate < DateTime.Now) status = OrderStatus.Overdue;
                else if (x.OrderDate != null) status = OrderStatus.Shipping;
                else status = OrderStatus.InRealization;

                view.Status = status;

                return view;
            });

            return result;
        }

        public async Task<int> GetFilteredOrdersListViewCount(GetRequest req)
        {
            var result = await _context.GetCount(new GetQueryOptions<Order, OrderListViewBase>()
            {
                Includes = (IQueryable<Order> q) => q.Include(o => o.OrderDetails).ThenInclude(od => od.Product),
                Filter = req.Filters?.GetExpression<Order>(),
                Select = (IQueryable<Order> q) => q.Select(o => new OrderListViewBase()
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    RequiredDate = o.RequiredDate,
                    ShippedDate = o.ShippedDate,
                    Total = Convert.ToDouble(o.OrderDetails.Sum(od => od.UnitPrice * od.Quantity))
                }),
            });

            return result;
        }

        public async Task<Order> CreateOrder(CreateOrderRequest req)
        {
            var order = _mapper.Map<CreateOrderRequest, Order>(req);

            order.OrderDate = DateTime.Now.ToUniversalTime();

            var productIds = order.OrderDetails.Select(d => d.OrderId);

            var products = await _context.GetEntities<Product, int>(productIds);

            foreach (var product in products)
            {
                var orderCount = order.OrderDetails.FirstOrDefault(x => x.ProductId == product.Id).Quantity;

                if (product.UnitsInStock < orderCount)
                    return null; //TODO add not enough in stock handling

                product.UnitsInStock -= orderCount;
                product.UnitsOnOrder += orderCount;
            }

            order = _context.CreateEntity(order);

            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<bool> RemoveOrder(int orderId)
        {
            var order = await _context.GetEntity(new GetSingleQueryOptions<Order>() { 
                Filter = (Order o) => o.Id == orderId,
                Includes = (IQueryable<Order> q) => q.Include(o => o.OrderDetails).ThenInclude(od => od.Product)
            });

            if (order == null) return false;

            if (order.ShippedDate == null)
            {
                //Hasn't been delivered, in that case return to stock
                foreach(var od in order.OrderDetails)
                {
                    var product = od.Product;
                    product.UnitsOnOrder -= od.Quantity;
                    product.UnitsInStock += od.Quantity;
                }
            }

            var result = _context.DeleteEntity(order);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
