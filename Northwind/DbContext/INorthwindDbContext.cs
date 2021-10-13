using Microsoft.EntityFrameworkCore;
using Northwind.DbContexts.Queries;
using Northwind.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.DbContexts
{
    public interface INorthwindDbContext
    {
        Task<T> GetEntity<T>(DbSet<T> set, int id) where T : class;
        Task<T> GetEntity<T>(DbSet<T> set, GetQueryOptions<T> opt) where T : class;
        Task<IEnumerable<T>> GetEntities<T>(DbSet<T> set, IEnumerable<int> ids) where T : class;
        Task<IEnumerable<T>> GetEntities<T>(DbSet<T> set, GetQueryOptions<T> opt) where T : class;
        Task<IEnumerable<TResult>> GetEntities<T, TResult>(DbSet<T> set, GetQueryOptions<T, TResult> opt) where T : class;
        T CreateEntity<T>(DbSet<T> set, T entity) where T : class;
        IEnumerable<T> CreateEntities<T>(DbSet<T> set, IEnumerable<T> entities) where T : class;
        T UpdateEntity<T>(DbSet<T> set, T entity) where T : class;
        T DeleteEntity<T>(DbSet<T> set, T entity) where T : class;
        IEnumerable<T> DeleteEntities<T>(DbSet<T> set, IEnumerable<T> entity) where T : class;

        Task SaveChangesAsync();
        DbSet<T> GetDbSet<T>() where T : class;

        DbSet<Category> Categories { get; }
        DbSet<Customer> Customers { get; }
        DbSet<Demographic> Demographics { get; }
        DbSet<Employee> Employees { get; }
        DbSet<Order> Orders { get; }
        DbSet<OrderDetails> OrderDetails { get; }
        DbSet<Product> Products { get; }
        DbSet<Region> Regions { get; }
        DbSet<Shipper> Shippers { get; }
        DbSet<Supplier> Suppliers { get; }
        DbSet<Territory> Territories { get; }
        
    }
}
