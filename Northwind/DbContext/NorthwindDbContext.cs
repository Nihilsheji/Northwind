using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Northwind.DbContexts.Queries;
using Northwind.Models.Entities;
using Northwind.Models.Interfaces;

namespace Northwind.DbContexts
{
    public class NorthwindDbContext : DbContext, INorthwindDbContext
    {
        public NorthwindDbContext(DbContextOptions<NorthwindDbContext> options) : base(options) { 
        
        }

        static NorthwindDbContext()
        {
            var props = typeof(NorthwindDbContext).GetProperties();

            foreach(var p in props)
            {
                var type = p.PropertyType;
                var dbSetType = typeof(DbSet<>);

                if(type.IsGenericType && type.GetGenericTypeDefinition() == dbSetType)
                {
                    var genericArg = type.GetGenericArguments()[0];
                    SetDictionary.Add(genericArg, p);
                }
            }
        }

        private static Dictionary<Type, PropertyInfo> SetDictionary { get; set; } = new Dictionary<Type, PropertyInfo>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>(e =>
            {
                e.HasKey(e => e.Id);

                e.Property(e => e.Id).HasColumnName("CategoryID");
                e.Property(e => e.Name).HasColumnName("CategoryName").HasColumnType("varchar(15)");
                e.Property(e => e.Description).HasColumnType("text");
                e.Property(e => e.Picture).HasColumnType("varchar(40)");

                e.HasMany(x => x.Products).WithOne(x => x.Category);
            });

            builder.Entity<Customer>(e =>
            {
                e.HasKey(e => e.Id);

                e.Property(e => e.Id).HasColumnName("CustomerID").HasColumnType("varchar(5)");
                e.Property(e => e.CompanyName).HasColumnType("varchar(40)");
                e.Property(e => e.ContactName).HasColumnType("varchar(30)");
                e.Property(e => e.ContactTitle).HasColumnType("varchar(30)");
                e.Property(e => e.Address).HasColumnType("varchar(60)");
                e.Property(e => e.City).HasColumnType("varchar(15)");
                e.Property(e => e.Region).HasColumnType("varchar(15)");
                e.Property(e => e.PostalCode).HasColumnType("varchar(10)");
                e.Property(e => e.Country).HasColumnType("varchar(15)");
                e.Property(e => e.Phone).HasColumnType("varchar(24)");
                e.Property(e => e.Fax).HasColumnType("varchar(24)");

                e.HasMany(x => x.Orders).WithOne(x => x.Customer);
                e.HasMany(x => x.Demographics).WithMany(x => x.Customers);
            });

            builder.Entity<Demographic>(e =>
            {
                e.HasKey(x => x.Id);

                e.Property(x => x.Id).HasColumnName("CustomerTypeID");

                e.HasMany(x => x.Customers).WithMany(x => x.Demographics);
            });

            builder.Entity<Employee>(e =>
            {
                e.HasKey(x => x.Id);

                e.Property(x => x.Id).HasColumnName("EmployeeID");
                e.Property(x => x.LastName).HasColumnType("varchar(20)");
                e.Property(x => x.FirstName).HasColumnType("varchar(10)");
                e.Property(x => x.Title).HasColumnType("varchar(30)");
                e.Property(x => x.TitleOfCourtesy).HasColumnType("varchar(25)");
                e.Property(x => x.Address).HasColumnType("varchar(60)");
                e.Property(x => x.City).HasColumnType("varchar(15)");
                e.Property(x => x.Region).HasColumnType("varchar(15)");
                e.Property(x => x.PostalCode).HasColumnType("varchar(10)");
                e.Property(x => x.Country).HasColumnType("varchar(15)");
                e.Property(x => x.HomePhone).HasColumnType("varchar(24)");
                e.Property(x => x.Extension).HasColumnType("varchar(4)");
                e.Property(x => x.Photo).HasColumnType("varchar(40)");
                e.Property(x => x.Notes).HasColumnType("text");
                e.Property(x => x.ReportsToId).HasColumnName("ReportsTo");

                e.HasMany(x => x.Subordinates).WithOne(x => x.ReportsTo);
                e.HasMany(x => x.Territories).WithMany(x => x.Employees);
                e.HasMany(x => x.Orders).WithOne(x => x.Employee);
            });

            builder.Entity<Order>(e =>
            {
                e.HasKey(x => x.Id);

                e.Property(x => x.Id).HasColumnName("OrderID");

                e.Property(x => x.CustomerId).HasColumnName("CustomerID").HasColumnType("varchar(5)");
                e.Property(x => x.EmployeeId).HasColumnName("EmployeeID");
                e.Property(x => x.ShipViaId).HasColumnName("ShipVia");
                e.Property(x => x.Freight).HasColumnType("real").HasDefaultValue(0);
                e.Property(x => x.ShipName).HasColumnType("varchar(40)");
                e.Property(x => x.ShipAddress).HasColumnType("varchar(60)");
                e.Property(x => x.ShipCity).HasColumnType("varchar(15)");
                e.Property(x => x.ShipRegion).HasColumnType("varchar(15)");
                e.Property(x => x.ShipPostalCode).HasColumnType("varchar(10)");
                e.Property(x => x.ShipCountry).HasColumnType("varchar(15)");

                e.HasOne(x => x.Customer).WithMany(x => x.Orders);
                e.HasOne(x => x.Employee).WithMany(x => x.Orders);
                e.HasOne(x => x.ShipVia).WithMany(x => x.Orders);
                e.HasMany(x => x.OrderDetails).WithOne(x => x.Order).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<OrderDetails>(e =>
            {
                e.HasKey(x => x.Id);

                e.Property(x => x.Id).HasColumnName("odID");
                e.Property(x => x.OrderId).HasColumnName("OrderID").HasDefaultValue(0);
                e.Property(x => x.ProductId).HasColumnName("ProductID").HasDefaultValue(0);

                e.Property(x => x.Discount).HasColumnType("real").HasDefaultValue(0);
                e.Property(x => x.UnitPrice).HasColumnType("real").HasDefaultValue(0);
                e.Property(x => x.Quantity).HasColumnType("smallint").HasDefaultValue(1);

                e.HasOne(x => x.Order).WithMany(x => x.OrderDetails).HasForeignKey(x => x.OrderId);
                e.HasOne(x => x.Product).WithMany(x => x.OrderDetails).HasForeignKey(x => x.ProductId);
            });

            builder.Entity<Product>(e =>
            {
                e.HasKey(x => x.Id);

                e.Property(x => x.Id).HasColumnName("ProductID");
                e.Property(x => x.ProductName).HasColumnType("varchar(40)");
                e.Property(x => x.SupplierId).HasColumnName("SupplierID");
                e.Property(x => x.CategoryId).HasColumnName("CategoryID");
                e.Property(x => x.QuantityPerUnit).HasColumnType("varchar(20)");
                e.Property(x => x.UnitPrice).HasColumnType("real").HasDefaultValue(0);
                e.Property(x => x.UnitsInStock).HasColumnType("smallint").HasDefaultValue(0);
                e.Property(x => x.UnitsOnOrder).HasColumnType("smallint").HasDefaultValue(0);
                e.Property(x => x.ReorderLevel).HasColumnType("smallint").HasDefaultValue(0);
                e.Property(x => x.Discontinued).HasColumnType("tinyint").HasDefaultValue(0);

                e.HasOne(x => x.Category).WithMany(x => x.Products);
                e.HasOne(x => x.Supplier).WithMany(x => x.Products);
                e.HasMany(x => x.OrderDetails).WithOne(x => x.Product);
            });

            builder.Entity<Region>(e =>
            {
                e.HasKey(x => x.Id);

                e.Property(x => x.Id).HasColumnName("RegionID");

                e.HasMany(x => x.Territories).WithOne(x => x.Region).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Shipper>(e =>
            {
                e.HasKey(x => x.Id);

                e.Property(x => x.Id).HasColumnName("ShipperID");
                e.Property(x => x.CompanyName).HasColumnType("varchar(40)");
                e.Property(x => x.Phone).HasColumnType("varchar(24)");

                e.HasMany(x => x.Orders).WithOne(x => x.ShipVia);
            });

            builder.Entity<Supplier>(e =>
            {
                e.HasKey(x => x.Id);

                e.Property(x => x.Id).HasColumnName("SupplierID");
                e.Property(x => x.CompanyName).HasColumnType("varchar(40)");
                e.Property(x => x.ContactName).HasColumnType("varchar(30)");
                e.Property(x => x.ContactTitle).HasColumnType("varchar(30)");
                e.Property(x => x.Address).HasColumnType("varchar(60)");
                e.Property(x => x.City).HasColumnType("varchar(15)");
                e.Property(x => x.Region).HasColumnType("varchar(15)");
                e.Property(x => x.PostalCode).HasColumnType("varchar(10)");
                e.Property(x => x.Country).HasColumnType("varchar(15)");
                e.Property(x => x.Phone).HasColumnType("varchar(24)");
                e.Property(x => x.Fax).HasColumnType("varchar(24)");
                e.Property(x => x.HomePage).HasColumnType("text");

                e.HasMany(x => x.Products).WithOne(x => x.Supplier);
            });

            builder.Entity<Territory>(e =>
            {
                e.HasKey(x => x.Id);

                e.Property(x => x.Id).HasColumnName("TerritoryID");
                e.Property(x => x.RegionId).HasColumnName("RegionID");

                e.HasMany(x => x.Employees).WithMany(x => x.Territories);
                e.HasOne(x => x.Region).WithMany(x => x.Territories);
            });

        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Demographic> Demographics { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Shipper> Shippers { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Territory> Territories { get; set; }

        public async Task<int> GetCount<T>() where T : class
        {
            var set = GetDbSet<T>();

            return await set.CountAsync();
        }

        public async Task<int> GetCount<T>(GetQueryOptions<T> opt) where T : class
        {
            var set = GetDbSet<T>();

            var query = set.AsQueryable<T>();

            query = opt.BuildQuery(query);

            return await query.CountAsync();
        }

        public async Task<int> GetCount<T, TResult>(GetQueryOptions<T, TResult> opt) where T : class
        {
            var set = GetDbSet<T>();

            var query = set.AsQueryable<T>();

            var projectedQuery = opt.BuildQuery(query);

            return await projectedQuery.CountAsync();
        }

        public async Task<T> GetEntity<T, KeyType>(KeyType id) where T : class
        {
            var set = GetDbSet<T>();

            return await set.FindAsync(id);
        }

        public async Task<T> GetEntity<T>(GetSingleQueryOptions<T> opt) where T : class
        {
            var set = GetDbSet<T>();

            var query = set.AsQueryable<T>();

            query = opt.BuildQuery(query);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<TResult> GetEntity<T, TResult>(GetSingleQueryOptions<T, TResult> opt) where T : class
        {
            var set = GetDbSet<T>();

            var query = set.AsQueryable<T>();

            var projectedQuery = opt.BuildQuery(query);

            return await projectedQuery.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetEntities<T>() where T : class
        {
            var set = GetDbSet<T>();

            return await set.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetEntities<T, KeyType>(IEnumerable<KeyType> ids) where T : class, IIdentificable<KeyType>
        {
            var set = GetDbSet<T>();

            return await set.Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetEntities<T>(GetQueryOptions<T> opt) where T : class
        {
            var set = GetDbSet<T>();

            var query = set.AsQueryable();

            query = opt.BuildQuery(query);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TResult>> GetEntities<T, TResult>(GetQueryOptions<T, TResult> opt) where T : class
        {
            var set = GetDbSet<T>();

            var query = set.AsQueryable();

            var projectedQuery = opt.BuildQuery(query);

            return await projectedQuery.ToListAsync();
        }

        public T CreateEntity<T>(T entity) where T : class
        {
            var set = GetDbSet<T>();

            var result = set.Add(entity);

            return result.Entity;
        }

        public IEnumerable<T> CreateEntities<T>(IEnumerable<T> entities) where T : class
        {
            var set = GetDbSet<T>();

            set.AddRange(entities);

            return entities;
        }

        public T UpdateEntity<T>(T entity) where T : class
        {
            var set = GetDbSet<T>();

            var result = set.Update(entity);

            return result.Entity;
        }

        public T DeleteEntity<T>(T entity) where T : class
        {
            var set = GetDbSet<T>();

            var result = set.Remove(entity);

            return result.Entity;
        }

        public IEnumerable<T> DeleteEntities<T>(IEnumerable<T> entities) where T : class
        {
            var set = GetDbSet<T>();

            set.RemoveRange(entities);

            return entities;
        }

        public async Task SaveChangesAsync()
        {
            await base.SaveChangesAsync();
        }

        private DbSet<T> GetDbSet<T>() where T : class
        {
            Type t = typeof(T);

            var set = SetDictionary[t]?.GetValue(this) as DbSet<T> ?? null;
            
            return set;           
        }
    }
}
