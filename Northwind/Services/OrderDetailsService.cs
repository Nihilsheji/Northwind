using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Northwind.DbContexts;
using Northwind.DbContexts.Queries;
using Northwind.Models.Entities;
using Northwind.Models.Request.OrderDetails;
using Northwind.Models.Response;
using Northwind.Services.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Services
{
    public class OrderDetailsService : CrudServiceBase<OrderDetails, int>, IOrderDetailsService
    {
        private readonly INorthwindDbContext _context;
        private readonly IMapper _mapper;
        public OrderDetailsService(INorthwindDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> RemoveOrderDetails(RemoveOrderDetailsRequest req)
        {
            var product = await _context.GetEntity<Product, int>(req.ProductId);

            var details = await _context.GetEntity(new GetSingleQueryOptions<OrderDetails>()
            {
                Filter = (OrderDetails d) => d.OrderId == req.OrderId && d.ProductId == req.ProductId
            });

            product.UnitsOnOrder -= details.Quantity;
            product.UnitsInStock += details.Quantity;

            var result = _context.DeleteEntity<OrderDetails>(details);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<OrderDetails> UpdateOrderDetails(UpdateOrderDetailsRequest req)
        {
            var product = await _context.GetEntity<Product, int>(req.ProductId);

            var details = await _context.GetEntity(new GetSingleQueryOptions<OrderDetails>()
            {
                Filter = (OrderDetails d) => d.OrderId == req.OrderId && d.ProductId == req.ProductId
            });

            var difference = req.Quantity - details.Quantity;

            product.UnitsOnOrder += difference;
            product.UnitsInStock -= difference;

            details.Quantity = req.Quantity;
            details.Discount = req.Discount;
            details.UnitPrice = req.UnitPrice; //Handle price assingment

            var result = _context.UpdateEntity(details);

            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<OrderDetails> CreateOrderDetails(CreateOrderDetailsRequest req)
        {
            var product = await _context.GetEntity<Product, int>(req.ProductId);

            var details = await _context.GetEntity(new GetSingleQueryOptions<OrderDetails>()
            {
                Filter = (OrderDetails d) => d.OrderId == req.OrderId && d.ProductId == req.ProductId
            });

            var difference = req.Quantity;

            product.UnitsOnOrder += difference;
            product.UnitsInStock -= difference;

            details.ProductId = req.ProductId;
            details.OrderId = req.OrderId;
            details.Quantity = req.Quantity;
            details.Discount = req.Discount;
            details.UnitPrice = req.UnitPrice;

            var result = _context.CreateEntity(details);

            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<IEnumerable<OrderDetailsListView>> GetOrderDetailsListViewForOrder(int orderId)
        {
            var details = await _context.GetEntities(new GetQueryOptions<Order, OrderDetailsListView>()
            {
                Filter = (Order o) => o.Id == orderId,
                Includes = (IQueryable<Order> q) => 
                    q.Include((Order o) => o.OrderDetails)
                    .ThenInclude((OrderDetails od) => od.Product),
                Select = (IQueryable<Order> q) =>
                    q.SelectMany((Order o) => o.OrderDetails.Select(od => new OrderDetailsListView()
                    {
                        OrderId = od.OrderId,
                        ProductId = od.ProductId,
                        ProductName = od.Product.ProductName,
                        UnitPrice = od.UnitPrice,
                        Quantity = od.Quantity                        
                    }))
            });

            return details;
        }

    }
}
