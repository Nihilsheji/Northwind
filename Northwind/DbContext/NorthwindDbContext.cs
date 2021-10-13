using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Northwind.DbContexts.Queries;
using Northwind.Models.Entities;

namespace Northwind.DbContexts
{
    public class NorthwindDbContext : DbContext, INorthwindDbContext
    {
        public NorthwindDbContext(DbContextOptions<NorthwindDbContext> options): base(options) {
            //SetMap = new Dictionary<Type, object>();

            //PropertyInfo[] properties = typeof(NorthwindDbContext).GetProperties(BindingFlags.Public);

            //foreach(PropertyInfo p in properties)
            //{
            //    if(IsInstanceOfGenericType(typeof(DbSet<>), p.PropertyType)) 
            //    { 
                    
            //    }
            //}
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Northwind");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>(e => {
                e.HasKey(e => e.Id);

                e.Property(e => e.Id).HasColumnName("CategoryID");
                e.Property(e => e.Name).HasColumnName("CategoryName");

                e.HasMany(x => x.Products).WithOne(x => x.Category);
            });

            builder.Entity<Customer>(e => {
                e.HasKey(e => e.Id);

                e.Property(e => e.Id).HasColumnName("CustomerID");
                //TODO add string constraints

                e.HasMany(x => x.Orders).WithOne(x => x.Customer);
                e.HasMany(x => x.Demographics).WithMany(x => x.Customers);
            });

            builder.Entity<Demographic>(e => {
                e.HasKey(x => x.Id);

                e.Property(x => x.Id).HasColumnName("CustomerTypeID");

                e.HasMany(x => x.Customers).WithMany(x => x.Demographics);
            });

            builder.Entity<Employee>(e => {
                e.HasKey(x => x.Id);

                e.Property(x => x.Id).HasColumnName("EmployeeID");
                e.Property(x => x.ReportsToId).HasColumnName("ReportsTo");

                e.HasMany(x => x.Subordinates).WithOne(x => x.ReportsTo);
                e.HasMany(x => x.Territories).WithMany(x => x.Employees);
                e.HasMany(x => x.Orders).WithOne(x => x.Employee);
            });

            builder.Entity<Order>(e => {
                e.HasKey(x => x.Id);

                e.Property(x => x.Id).HasColumnName("OrderID");

                e.HasOne(x => x.Customer).WithMany(x => x.Orders);
                e.HasOne(x => x.Employee).WithMany(x => x.Orders);
                e.HasOne(x => x.ShipVia).WithMany(x => x.Orders);
                e.HasMany(x => x.OrderDetails).WithOne(x => x.Order);
            });

            builder.Entity<OrderDetails>(e => {
                e.HasKey(x => new { x.OrderId, x.ProductId });

                e.HasOne(x => x.Order).WithMany(x => x.OrderDetails);
                e.HasOne(x => x.Product).WithMany(x => x.OrderDetails);
            });

            builder.Entity<Product>(e => {
                e.HasKey(x => x.Id);

                e.Property(x => x.Id).HasColumnName("ProductID");

                e.HasOne(x => x.Category).WithMany(x => x.Products);
                e.HasOne(x => x.Supplier).WithMany(x => x.Products);
                e.HasMany(x => x.OrderDetails).WithOne(x => x.Product);
            });

            builder.Entity<Region>(e => {
                e.HasKey(x => x.Id);

                e.Property(x => x.Id).HasColumnName("RegionID");

                e.HasMany(x => x.Territories).WithOne(x => x.Region);
            });

            builder.Entity<Shipper>(e => {
                e.HasKey(x => x.Id);

                e.Property(x => x.Id).HasColumnName("ShipperID");

                e.HasMany(x => x.Orders).WithOne(x => x.ShipVia);
            });

            builder.Entity<Supplier>(e => {
                e.HasKey(x => x.Id);

                e.Property(x => x.Id).HasColumnName("SupplierID");

                e.HasMany(x => x.Products).WithOne(x => x.Supplier);
            });

            builder.Entity<Territory>(e => {
                e.HasKey(x => x.Id);

                e.Property(x => x.Id).HasColumnName("TerritoryID");

                e.HasMany(x => x.Employees).WithMany(x => x.Territories);
                e.HasOne(x => x.Region).WithMany(x => x.Territories);
            });

        }

        private Dictionary<Type, object> SetMap;

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

        //public delegate GetPredicate = new Func<, T>

        //public async Task<IEnumerable<T>> GetEntities<T>(DbSet<T> set, GetEntityOptions<T)
        //{
        //    if(set.)
        //    set.Where<>
        //}

        //    public T GetEntity<T>(DbSet<T> set, GetEntityOptions<T> opt)
        //    {
        //        IQueryable<T> result = set.AsQueryable<T>();
        //        if (opt.Where != null)
        //        {
        //            result = set.Where(opt.Where);
        //        }

        //        if(opt.Count != 0)
        //        {                
        //            result = result.Take(opt.Count);
        //        }

        //        if(opt.)


        //        return await result.ToListAsync();
        //    }

        //}

        //public class GetEntityOptions<T>
        //{
        //    public Func<T, bool> Where { get; set; }
        //    public int Count { get; set; }
        //    public int Skip { get; set; }
        //    public Func<T, int> Sort { get; set; }
        //}

        //public class GetEntityOptions<T, T2>
        //{
        //    public Func<T, bool> Where { get; set; }
        //    public int Count { get; set; }
        //    public int Skip { get; set; }
        //    public Func<T, T2> Map { get; set; }
        //    public Func<T, int> Sort { get; set; }

        //}

        public async Task<T> GetEntity<T> (DbSet<T> set, int id) where T : class
        {
            return await set.FindAsync(id);
        }

        public async Task<T> GetEntity<T> (DbSet<T> set, GetQueryOptions<T> opt) where T : class
        {
            var query = set.AsQueryable();

            if (opt.Filter != null)
            {
                query = query.Where(opt.Filter);
            }

            if (opt.Reverse)
            {
                query = query.Reverse();
            }

            if (opt.Count != 0)
            {
                query = query.Take(opt.Count);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetEntities<T> (DbSet<T> set, IEnumerable<int> ids) where T : class {
            return await set.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetEntities<T>(DbSet<T> set, GetQueryOptions<T> opt) where T : class
        {
            var query = set.AsQueryable();

            if(opt.Includes != null)
            {
                query = opt.Includes(query);
            }            

            if (opt.Filter != null)
            {
                query = query.Where(opt.Filter);
            }

            if (opt.Reverse)
            {
                query = query.Reverse();
            }

            if (opt.Count != 0)
            {
                query = query.Take(opt.Count);
            }            

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TResult>> GetEntities<T, TResult>(DbSet<T> set, GetQueryOptions<T, TResult> opt) where T : class
        {
            var query = set.AsQueryable();

            if (opt.Includes != null)
            {
                query = opt.Includes(query);
            }

            if (opt.Filter != null)
            {
                query = query.Where(opt.Filter);
            }

            if (opt.Reverse)
            {
                query = query.Reverse();
            }

            if (opt.Count != 0)
            {
                query = query.Take(opt.Count);
            }

            var projectedQuery = opt.Select(query);

            return await projectedQuery.ToListAsync();
        }

        public T CreateEntity<T>(DbSet<T> set, T entity) where T: class
        {
            var result = set.Add(entity);
            return result.Entity;
        }

        public IEnumerable<T> CreateEntities<T>(DbSet<T> set, IEnumerable<T> entities) where T : class
        {
            set.AddRange(entities);
            return entities;
        }

        public T UpdateEntity<T>(DbSet<T> set, T entity) where T : class
        {
            var result = set.Update(entity);
            return result.Entity;
        }

        public T DeleteEntity<T>(DbSet<T> set, T entity) where T : class
        {
            var result = set.Remove(entity);
            return result.Entity;
        }

        public IEnumerable<T> DeleteEntities<T>(DbSet<T> set, IEnumerable<T> entities) where T : class
        {
            set.RemoveRange(entities);
            return entities;
        }

        public async Task SaveChangesAsync()
        {
            await base.SaveChangesAsync();
        }

        public DbSet<T> GetDbSet<T>() where T : class
        {
            Type t = typeof(T);
            if(t == typeof(Category))
                return Categories as DbSet<T>;

            if (t == typeof(Shipper))
                return Shippers as DbSet<T>;

            return null;
        }

        static bool IsInstanceOfGenericType(Type genericType, Type type)
        {
            while (type != null)
            {
                if (type.IsGenericType &&
                    type.GetGenericTypeDefinition() == genericType)
                {
                    return true;
                }
                type = type.BaseType;
            }
            return false;
        }

    }
}
