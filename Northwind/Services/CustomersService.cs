using Northwind.DbContexts;
using Northwind.DbContexts.Queries;
using Northwind.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Northwind.Services.Abstractions;
using Northwind.Models.Response;
using Northwind.Models.Request.Customer;
using AutoMapper;
using Northwind.Models.Request.Demographic;

namespace Northwind.Services
{
    public class CustomersService : CrudServiceBase<Customer, string>, ICustomersService
    {
        private readonly INorthwindDbContext _context;
        private readonly IMapper _mapper;

        public CustomersService(INorthwindDbContext context, IMapper mapper) : base(context) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Customer>> GetCustomersForDemographic(int demographicId)
        {
            var demo = await _context.GetEntity(new GetSingleQueryOptions<Demographic>()
            {
                Filter = (Demographic d) => d.Id == demographicId,
                Includes = (IQueryable<Demographic> q) =>
                    q.Include(d => d.Customers)
            });

            return demo.Customers;
        }

        public async Task<IEnumerable<CustomerListView>> GetCustomersListViewForDemographic(int demographicId)
        {
            var customers = await _context.GetEntities(new GetQueryOptions<Demographic, CustomerListView>()
            {
                Count = 1,
                Filter = (Demographic d) => d.Id == demographicId,
                Includes = (IQueryable<Demographic> q) =>
                    q.Include(d => d.Customers),
                Select = (IQueryable<Demographic> q) => q.SelectMany(d => d.Customers.Select(c => new CustomerListView()
                {
                    Id = c.Id,
                    CompanyName = c.CompanyName,
                    ContactName = c.ContactName
                }))
            });

            return customers;
        }

        public async Task<IEnumerable<DictionaryValue<string, string>>> GetDictionary()
        {
            return await GetDictionary((Customer c) => new DictionaryValue<string, string>() { Key = c.Id, Value = c.CompanyName });
        }

        public async Task<IEnumerable<CustomerListView>> GetCustomersListView()
        {
            var customers = await _context.GetEntities(new GetQueryOptions<Customer, CustomerListView>()
            {
                Select = (IQueryable<Customer> q) => q.Select(c => new CustomerListView()
                {
                    Id = c.Id,
                    CompanyName = c.CompanyName,
                    ContactName = c.ContactName
                })
            });

            return customers;
        }

        public async Task<Customer> CreateCustomer(CreateCustomerRequest req)
        {
            var existing = await _context.GetEntities<Demographic, int>(req.ExistingDemographics);

            var customer = _mapper.Map<CreateCustomerRequest, Customer>(req);

            foreach (var d in existing)
            {
                customer.Demographics.Add(d);
            }

            customer = _context.CreateEntity(customer);

            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task<bool> AddDemographicToCustomer(string customerId, int demographicId)
        {
            var cust = await _context.GetEntity<Customer, string>(customerId);

            var demo = await _context.GetEntity<Demographic, int>(demographicId);

            if (cust == null || demo == null) return false;

            if (cust.Demographics == null)
                cust.Demographics = new List<Demographic>();

            cust.Demographics.Add(demo);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveDemographicFromCustomer(string customerId, int demographicId)
        {
            var cust = await _context.GetEntity(new GetSingleQueryOptions<Customer>()
            {
                Filter = (Customer c) => c.Id == customerId,
                Includes = (IQueryable<Customer> q) =>
                    q.Include(c => c.Demographics)
            });

            var demo = await _context.GetEntity<Demographic, int>(demographicId);

            if (cust == null || demo == null) return false;

            if (cust.Demographics == null) return false;

            cust.Demographics.Remove(demo);

            await _context.SaveChangesAsync();

            return true;
        }

    }
}
