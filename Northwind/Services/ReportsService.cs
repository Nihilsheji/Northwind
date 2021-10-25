using Microsoft.EntityFrameworkCore;
using Northwind.DbContexts;
using Northwind.DbContexts.Queries;
using Northwind.Models.Entities;
using Northwind.Models.Request;
using Northwind.Models.Response;
using Northwind.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Services
{
    public class ReportsService : IReportsService
    {
        private readonly INorthwindDbContext _context;

        public ReportsService(INorthwindDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductReportView>> GetBestSellingProducts()
        {
            var products = await _context.GetEntities(_context.GetDbSet<Product>(), new GetQueryOptions<Product, ProductReportView>()
            {
                Includes = (IQueryable<Product> q) =>
                    q.Include(p => p.OrderDetails).ThenInclude(od => od.Order),
                Count = 10,
                Select = (IQueryable<Product> q) =>
                    q.Select(a => new ProductReportView() { 
                        Id = a.Id,
                        Name = a.ProductName,
                        Total = a.OrderDetails.Select(x => x.Quantity * x.UnitPrice).Sum()
                    }).OrderByDescending(x => x.Total)
            });

            return products;
        }

        public async Task<IEnumerable<ProductReportView>> GetMostSellingProducts()
        {
            var products = await _context.GetEntities(_context.GetDbSet<Product>(), new GetQueryOptions<Product, ProductReportView>()
            {
                Includes = (IQueryable<Product> q) =>
                    q.Include(p => p.OrderDetails).ThenInclude(od => od.Order),
                Count = 10,
                Select = (IQueryable<Product> q) =>
                    q.Select(a => new ProductReportView()
                    {
                        Id = a.Id,
                        Name = a.ProductName,
                        Total = a.OrderDetails.Select(x => x.Quantity).Sum()
                    }).OrderByDescending(x => x.Total)
            });

            return products;
        }

        public async Task<IEnumerable<ProductSalesView>> GetProductSaleStatistics(int productId, GetStatisticsRequest req)
        {

            var products = await _context.GetEntities(_context.GetDbSet<Product>(), new GetQueryOptions<Product, ProductSalesViewBase>()
            {
                Includes = (IQueryable<Product> q) =>
                    q.Include(p => p.OrderDetails).ThenInclude(od => od.Order),
                Filter = (Product p) => p.Id == productId,
                Count = 10,
                Select = (IQueryable<Product> q) =>
                    q.SelectMany(a => a.OrderDetails.Select(x =>
                        new { Order = x.Order, Count = x.Quantity })
                    .Select(x => new ProductSalesViewBase()
                    {
                        Date = x.Order.OrderDate,
                        Count = x.Count
                    }))
            });

            IEnumerable<ProductSalesView> result = null;

            if(req.Span == StatisticsSpan.Year)
            {
                result = products.GroupBy(x => new { Year = x.Date.Value!.Year }).Select(x => new ProductSalesView()
                {
                    Count = x.Sum(y => y.Count),
                    Year = x.Key.Year
                });
            } 
            else if(req.Span == StatisticsSpan.Month)
            {
                result = products.GroupBy(x => new { Year = x.Date.Value!.Year, Month = x.Date.Value!.Month }).Select(x => new ProductSalesView()
                {
                    Count = x.Sum(y => y.Count),
                    Year = x.Key.Year,
                    Month = x.Key.Month
                });
            } 
            else if(req.Span == StatisticsSpan.Day)
            {
                result = products.GroupBy(x => new { Year = x.Date.Value!.Year, Month = x.Date.Value!.Month, Day = x.Date.Value!.Day })
                    .Select(x => new ProductSalesView()
                    {
                        Count = x.Sum(y => y.Count),
                        Year = x.Key.Year,
                        Month = x.Key.Month,
                        Day = x.Key.Day
                    });
            }

            return result;
        }

        public async Task<IEnumerable<EmployeeReportView>> GetBestSellingEmployees()
        {
            var employees = await _context.GetEntities(_context.GetDbSet<Employee>(), new GetQueryOptions<Employee, EmployeeReportView>()
            {
                Includes = (IQueryable<Employee> q) =>
                    q.Include(p => p.Orders).ThenInclude(o => o.OrderDetails),
                Count = 10,
                Select = (IQueryable<Employee> q) =>
                    q.Select(e => new EmployeeReportView()
                    {
                        Id = e.Id,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        Total = e.Orders.SelectMany(o => o.OrderDetails.Select(od => od.Quantity * od.UnitPrice)).Sum()
                    }).OrderByDescending(x => x.Total)
            });

            return employees;
        }

        public async Task<IEnumerable<CustomerReportView>> GetBestBuyingCustomers()
        {
            var customers = await _context.GetEntities(_context.GetDbSet<Customer>(), new GetQueryOptions<Customer, CustomerReportView>()
            {
                Includes = (IQueryable<Customer> q) =>
                    q.Include(c => c.Orders).ThenInclude(o => o.OrderDetails),
                Count = 10,
                Select = (IQueryable<Customer> q) =>
                    q.Select(c => new CustomerReportView()
                    {
                        Id = c.Id,
                        Name = c.CompanyName,
                        Total = c.Orders.SelectMany(o => o.OrderDetails.Select(od => od.Quantity * od.UnitPrice)).Sum()
                    }).OrderByDescending(x => x.Total)
            });

            return customers;
        }

        public async Task<IEnumerable<PopularityReportView>> GetMostPopularCategories()
        {
            var categories = await _context.GetEntities(_context.GetDbSet<Category>(), new GetQueryOptions<Category, PopularityReportView>()
            {
                Includes = (IQueryable<Category> q) => q.Include(c => c.Products).ThenInclude(p => p.OrderDetails),
                Select = (IQueryable<Category> q) => 
                    q.Select(c => new { Id = c.Id, Count = c.Products.SelectMany(p => p.OrderDetails.Select(od => 1)).Count() })
                    .Select(x => new PopularityReportView()
                    {
                        Id = x.Id,
                        Count = x.Count
                    }).OrderByDescending(x => x.Count)
            });

            return categories;
        }
    }
}
