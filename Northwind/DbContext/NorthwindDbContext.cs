using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Northwind.DbContexts.Queries;
using Northwind.Models.Entities;

namespace Northwind.DbContexts
{
    public class NorthwindDbContext : DbContext, INorthwindDbContext
    {
        public NorthwindDbContext(DbContextOptions<NorthwindDbContext> options): base(options) {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>(e => {
                e.HasKey(e => e.Id);

                e.Property(e => e.Id).HasColumnName("CategoryID");
                e.Property(e => e.Name).HasColumnName("CategoryName").HasColumnType("varchar(15)");
                e.Property(e => e.Description).HasColumnType("text");
                e.Property(e => e.Picture).HasColumnType("varchar(40)");

                e.HasMany(x => x.Products).WithOne(x => x.Category);                
            });

            builder.Entity<Customer>(e => {
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

            builder.Entity<Demographic>(e => {
                e.HasKey(x => x.Id);

                e.Property(x => x.Id).HasColumnName("CustomerTypeID");

                e.HasMany(x => x.Customers).WithMany(x => x.Demographics);
            });

            builder.Entity<Employee>(e => {
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

            builder.Entity<Order>(e => {
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

            builder.Entity<OrderDetails>(e => {
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

            builder.Entity<Product>(e => {
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

            builder.Entity<Region>(e => {
                e.HasKey(x => x.Id);

                e.Property(x => x.Id).HasColumnName("RegionID");

                e.HasMany(x => x.Territories).WithOne(x => x.Region).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Shipper>(e => {
                e.HasKey(x => x.Id);

                e.Property(x => x.Id).HasColumnName("ShipperID");
                e.Property(x => x.CompanyName).HasColumnType("varchar(40)");
                e.Property(x => x.Phone).HasColumnType("varchar(24)");

                e.HasMany(x => x.Orders).WithOne(x => x.ShipVia);
            });

            builder.Entity<Supplier>(e => {
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

            builder.Entity<Territory>(e => {
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

        public async Task<int> GetCount<T>(DbSet<T> set) where T : class
        {
            return await set.CountAsync();
        }

        public async Task<int> GetCount<T>(DbSet<T> set, GetQueryOptions<T> opt) where T : class
        {
            var query = set.AsQueryable<T>();

            query = opt.BuildQuery(query);

            return await query.CountAsync();
        }

        public async Task<int> GetCount<T, TResult>(DbSet<T> set, GetQueryOptions<T, TResult> opt) where T : class
        {
            var query = set.AsQueryable<T>();

            query = opt.BuildQuery(query);

            return await query.CountAsync();
        }

        public async Task<T> GetEntity<T, KeyType> (DbSet<T> set, KeyType id) where T : class
        {
            return await set.FindAsync(id);
        }

        public async Task<T> GetEntity<T>(DbSet<T> set, GetSingleQueryOptions<T> opt) where T : class {
            var query = set.AsQueryable<T>();
            
            query = opt.BuildQuery(query);

            return await query.FirstOrDefaultAsync();
        }
        
        public async Task<TResult> GetEntity<T, TResult>(DbSet<T> set, GetSingleQueryOptions<T, TResult> opt) where T : class {
            var query = set.AsQueryable<T>();

            var projectedQuery = opt.BuildQuery(query);

            return await projectedQuery.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetEntities<T>(DbSet<T> set) where T : class
        {
            return await set.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetEntities<T, KeyType> (DbSet<T> set, IEnumerable<KeyType> ids) where T : class {
            return await set.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetEntities<T>(DbSet<T> set, GetQueryOptions<T> opt) where T : class
        {
            var query = set.AsQueryable();

            query = BuildQuery(query, opt);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TResult>> GetEntities<T, TResult>(DbSet<T> set, GetQueryOptions<T, TResult> opt) where T : class
        {
            var query = set.AsQueryable();

            query = BuildQuery(query, opt);

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
            if (t == typeof(Category))
                return Categories as DbSet<T>;

            if (t == typeof(Shipper))
                return Shippers as DbSet<T>;

            if (t == typeof(Customer))
                return Customers as DbSet<T>;

            if (t == typeof(Demographic))
                return Demographics as DbSet<T>;

            if (t == typeof(Employee))
                return Employees as DbSet<T>;

            if (t == typeof(Order))
                return Orders as DbSet<T>;

            if (t == typeof(OrderDetails))
                return OrderDetails as DbSet<T>;

            if (t == typeof(Product))
                return Products as DbSet<T>;

            if (t == typeof(Region))
                return Regions as DbSet<T>;

            if (t == typeof(Supplier))
                return Suppliers as DbSet<T>;

            if (t == typeof(Territory))
                return Territories as DbSet<T>;

            return null;
        }

        private static IQueryable<T> BuildQuery<T>(IQueryable<T> query, GetQueryOptions<T> opt) where T : class
        {            

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

            if (opt.Sort != null)
            {
                var first = true;
                foreach(var exp in opt.Sort)
                {
                    var type = exp.Type;
                    var funcType = typeof(Func<,>);
                    var expression = typeof(Expression<>).MakeGenericType(funcType);

                    var orderBy = typeof(Queryable).GetMethodExt("OrderBy", new[] { typeof(IQueryable<>), expression })
                        .MakeGenericMethod(typeof(T), type);
                    var orderByDesc = typeof(Queryable).GetMethodExt("OrderByDescending", new[] { typeof(IQueryable<>), expression })
                        .MakeGenericMethod(typeof(T), type);
                    var thenOrderBy = typeof(Queryable).GetMethodExt("ThenBy", new[] { typeof(IOrderedQueryable<>), expression })
                        .MakeGenericMethod(typeof(T), type);
                    var thenOrderByDesc = typeof(Queryable).GetMethodExt("ThenByDescending", new[] { typeof(IOrderedQueryable<>), expression })
                        .MakeGenericMethod(typeof(T), type);

                    if (first && exp.Ascending)
                    {
                        query = orderBy.Invoke(null, new object[] { query, exp.Expression }) as IQueryable<T>;
                        first = false;
                    } 
                    else if(first && !exp.Ascending) 
                    {
                        query = orderByDesc.Invoke(null, new object[] { query, exp.Expression }) as IQueryable<T>;
                        first = false;
                    }
                    else if (exp.Ascending) 
                    {
                        query = thenOrderBy.Invoke(null, new object[] { query, exp.Expression }) as IQueryable<T>;
                    }
                    else 
                    {
                        query = thenOrderByDesc.Invoke(null, new object[] { query, exp.Expression }) as IQueryable<T>;
                    }
                }
            }

            if (opt.Skip != 0)
            {
                query = query.Skip(opt.Skip);
            }

            if (opt.Count != 0)
            {
                query = query.Take(opt.Count);
            }                        

            return query;
        }
    }

    public static class TypeExt
    {
        public static MethodInfo GetMethodExt
        (
            this Type thisType,
            string name,
            params Type[] parameterTypes
        )
        {
            return GetMethodExt(thisType,
                                name,
                                BindingFlags.Instance
                                | BindingFlags.Static
                                | BindingFlags.Public
                                | BindingFlags.NonPublic
                                | BindingFlags.FlattenHierarchy,
                                parameterTypes);
        }

        public static MethodInfo GetMethodExt
        (
            this Type thisType,
            string name,
            BindingFlags bindingFlags,
            params Type[] parameterTypes
        )
        {
            MethodInfo matchingMethod = null;

            GetMethodExt(ref matchingMethod, thisType, name, bindingFlags, parameterTypes);

            if (matchingMethod == null && thisType.IsInterface)
            {
                foreach (Type interfaceType in thisType.GetInterfaces())
                    GetMethodExt(ref matchingMethod,
                                 interfaceType,
                                 name,
                                 bindingFlags,
                                 parameterTypes);
            }

            return matchingMethod;
        }

        private static void GetMethodExt(ref MethodInfo matchingMethod,
                                    Type type,
                                    string name,
                                    BindingFlags bindingFlags,
                                    params Type[] parameterTypes)
        {
            // Check all methods with the specified name, including in base classes
            foreach (MethodInfo methodInfo in type.GetMember(name,
                                                             MemberTypes.Method,
                                                             bindingFlags))
            {
                // Check that the parameter counts and types match, 
                // with 'loose' matching on generic parameters
                ParameterInfo[] parameterInfos = methodInfo.GetParameters();
                if (parameterInfos.Length == parameterTypes.Length)
                {
                    int i = 0;
                    for (; i < parameterInfos.Length; ++i)
                    {
                        if (!parameterInfos[i].ParameterType
                                              .IsSimilarType(parameterTypes[i]))
                            break;
                    }
                    if (i == parameterInfos.Length)
                    {
                        if (matchingMethod == null)
                            matchingMethod = methodInfo;
                        else
                            throw new AmbiguousMatchException(
                                   "More than one matching method found!");
                    }
                }
            }
        }

        private static bool IsSimilarType(this Type thisType, Type type)
        {
            // Ignore any 'ref' types
            if (thisType.IsByRef)
                thisType = thisType.GetElementType();
            if (type.IsByRef)
                type = type.GetElementType();

            // Handle array types
            if (thisType.IsArray && type.IsArray)
                return thisType.GetElementType().IsSimilarType(type.GetElementType());

            // If the types are identical, or they're both generic parameters 
            // or the special 'T' type, treat as a match
            if (thisType == type || ((thisType.IsGenericParameter || thisType == typeof(T))
                                 && (type.IsGenericParameter || type == typeof(T))))
                return true;

            // Handle any generic arguments
            if (thisType.IsGenericType && type.IsGenericType)
            {
                Type[] thisArguments = thisType.GetGenericArguments();
                Type[] arguments = type.GetGenericArguments();
                if (thisArguments.Length == arguments.Length)
                {
                    for (int i = 0; i < thisArguments.Length; ++i)
                    {
                        if (!thisArguments[i].IsSimilarType(arguments[i]))
                            return false;
                    }
                    return true;
                }
            }

            return false;
        }

        public class T
        { }
    }
}
