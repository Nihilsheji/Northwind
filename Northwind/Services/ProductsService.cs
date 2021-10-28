using Northwind.DbContexts;
using Northwind.DbContexts.Queries;
using Northwind.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Northwind.Services.Abstractions;
using Northwind.Models.Response;

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
            var result = await _context.GetEntities(
                new GetQueryOptions<Product>()
                {
                    Filter = (Product x) => x.SupplierId == supplierId
                });

            return result;
        }

        public async Task<IEnumerable<ProductListView>> GetProductsListViewForSupplier(int supplierId)
        {
            var result = await _context.GetEntities(
                new GetQueryOptions<Product, ProductListView>()
                {
                    Filter = (Product x) => x.SupplierId == supplierId,
                    Select = (IQueryable<Product> q) => q.Select((Product p) =>
                        new ProductListView()
                        {
                            Id = p.Id,
                            ProductName = p.ProductName,
                            UnitPrice = p.UnitPrice
                        }
                    )
                });

            return result;
        }

        public async Task<IEnumerable<Product>> GetProductsForCategory(int categoryId)
        {
            var result = await _context.GetEntities(
                new GetQueryOptions<Product>() {
                    Filter = (Product x) => x.CategoryId == categoryId
                });

            return result;
        }

        public async Task<IEnumerable<ProductListView>> GetProductsListViewForCategory(int categoryId)
        {
            var result = await _context.GetEntities(
                new GetQueryOptions<Product, ProductListView>()
                {
                    Filter = (Product x) => x.CategoryId == categoryId,
                    Select = (IQueryable<Product> q) => q.Select((Product p) => 
                        new ProductListView()
                        {
                            Id = p.Id,
                            ProductName = p.ProductName,
                            UnitPrice = p.UnitPrice
                        }
                    )
                });

            return result;
        }

        public async Task<IEnumerable<Product>> GetProductsForOrder(int orderId)
        {
            var result = await _context.GetEntities(new GetQueryOptions<Order, Product>()
            {
                Filter = (Order o) => o.Id == orderId,
                Count = 1,
                Includes = (IQueryable<Order> q) => q.Include(o => o.OrderDetails).ThenInclude(od => od.Product),
                Select = (IQueryable<Order> q) => q.SelectMany(o => o.OrderDetails.Select(od => od.Product))
            });

            return result;
        }

        public async Task<IEnumerable<DictionaryValue<int, string>>> GetDictionary()
        {
            return await GetDictionary<int, string>((Product p) => new DictionaryValue<int, string>() { Key = p.Id, Value = p.ProductName });
        }
    }
}
