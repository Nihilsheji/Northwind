using Northwind.DbContexts;
using Northwind.DbContexts.Queries;
using Northwind.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Northwind.Services.Abstractions;

namespace Northwind.Services
{
    public class ProductsService : CrudServiceBase<Product>, IProductsService
    {
        private readonly INorthwindDbContext _context;
        public ProductsService(INorthwindDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductsForSupplier(int supplierId)
        {
            var result = await _context.GetEntities(_context.GetDbSet<Product>(),
                new GetQueryOptions<Product>()
                {
                    Filter = (Product x) => x.SupplierId == supplierId
                });

            return result;
        }

        public async Task<IEnumerable<Product>> GetProductsForCategory(int categoryId)
        {
            var result = await _context.GetEntities(_context.GetDbSet<Product>(),
                new GetQueryOptions<Product>() {
                    Filter = (Product x) => x.CategoryId == categoryId
                });

            return result;
        }

        public async Task<IEnumerable<Product>> GetProductsForOrder(int orderId)
        {
            var result = await _context.GetEntities(_context.GetDbSet<Order>(), new GetQueryOptions<Order, Product>()
            {
                Filter = (Order o) => o.Id == orderId,
                Count = 1,
                Includes = (IQueryable<Order> q) => q.Include(o => o.OrderDetails).ThenInclude(od => od.Product),
                Select = (IQueryable<Order> q) => q.SelectMany(o => o.OrderDetails.Select(od => od.Product))
            });

            return result;
        }
    }
}
