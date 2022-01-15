using Microsoft.EntityFrameworkCore;
using Northwind.DbContexts;
using Northwind.DbContexts.Queries;
using Northwind.Models.Entities;
using Northwind.Models.Response;
using Northwind.Services.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Services
{
    public class DemographicsService : CrudServiceBase<Demographic, int>, IDemographicsService
    {
        private readonly INorthwindDbContext _context;

        public DemographicsService(INorthwindDbContext context) : base(context) {
            _context = context;
        }

        public async Task<IEnumerable<Demographic>> GetCustomersForDemographic(string demographicId)
        {
            var demo = await _context.GetEntity(new GetSingleQueryOptions<Customer>()
            {
                Filter = (Customer d) => d.Id == demographicId,
                Includes = (IQueryable<Customer> q) => 
                    q.Include(c => c.Demographics)
            });

            return demo.Demographics;
        }

        public async Task<IEnumerable<DictionaryValue<int, string>>> GetDictionary()
        {
            return await GetDictionary<int, string>((Demographic d) => new DictionaryValue<int, string>() { Key = d.Id, Value = d.CustomerDesc });
        }

        public async Task<bool> AddCustomerToDemographic(int demographicId, string customerId)
        {
            var demo = await _context.GetEntity<Demographic, int>(demographicId);

            var cust = await _context.GetEntity<Customer, string>(customerId);

            if (demo == null || cust == null) return false;

            if (demo.Customers == null)
                demo.Customers = new List<Customer>();

            demo.Customers.Add(cust);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveCustomerFromDemographic(int demographicId, string customerId)
        {
            var demo = await _context.GetEntity(new GetSingleQueryOptions<Demographic>()
            {
                Filter = (Demographic d) => d.Id == demographicId,
                Includes = (IQueryable<Demographic> q) =>
                    q.Include(d => d.Customers)
            });

            var cust = await _context.GetEntity<Customer, string>(customerId);

            if (demo == null || cust == null) return false;

            if (demo.Customers == null) return false;

            demo.Customers.Remove(cust);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
