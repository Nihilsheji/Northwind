using Northwind.DbContexts;
using Northwind.DbContexts.Queries;
using Northwind.Models.Entities;
using Northwind.Models.Response;
using Northwind.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Services
{
    public class SuppliersService : CrudServiceBase<Supplier>, ISuppliersService
    {
        private readonly INorthwindDbContext _context;
        public SuppliersService(INorthwindDbContext context) : base(context)
        {
            _context = context;
        }        

        public async Task<IEnumerable<DictionaryValue<int, string>>> GetDictionary()
        {
            return await GetDictionary(x => new DictionaryValue<int, string>() { Key = x.Id, Value = x.CompanyName });
        }

        public async Task<IEnumerable<SupplierListView>> GetSuppliersListView()
        {
            return await _context.GetEntities(_context.GetDbSet<Supplier>(), new GetQueryOptions<Supplier, SupplierListView>
            {
                Select = (IQueryable<Supplier> q) => q.Select((Supplier s) => new SupplierListView()
                {
                    Id = s.Id,
                    CompanyName = s.CompanyName,
                    ContactName = s.ContactName
                })
            });
        }

    }
}
