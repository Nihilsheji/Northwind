using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models.Entities;
using Northwind.Models.Request.Customer;
using Northwind.Models.Response;
using Northwind.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Controllers
{
    [ApiController]
    [Route("customers")]
    public class CustomersController : Controller
    {
        private readonly ICustomersService _customers;
        private readonly IMapper _mapper;
        
        public CustomersController(ICustomersService customers, IMapper mapper)
        {
            _customers = customers;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all customers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerView>>> GetCustomers()
        {
            var result = await _customers.Get();

            return Ok(_mapper.Map<IEnumerable<CustomerView>>(result));
        }

        /// <summary>
        /// Gets a customer
        /// </summary>
        /// <param name="customerId">Id of the customer</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{customerId}")]
        public async Task<ActionResult<CustomerView>> GetCustomer(string customerId)
        {
            var result = await _customers.Get(customerId);

            return Ok(_mapper.Map<CustomerView>(result));
        }

        /// <summary>
        /// Returns list view of customers for demographic
        /// </summary>
        /// <param name="demographicId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/demographics/{demographicId}/customers")]
        public async Task<ActionResult<IEnumerable<CustomerListView>>> GetCustomersListViewForDemographic(int demographicId)
        {
            var result = await _customers.GetCustomersListViewForDemographic(demographicId);

            return Ok(result);
        }

        /// <summary>
        /// Gets id, name dictionary for customers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("dictionary")]
        public async Task<ActionResult<IEnumerable<DictionaryValue<string, string>>>> GetDictionary()
        {
            var result = await _customers.GetDictionary();

            return Ok(result);
        }

        /// <summary>
        /// Gets id, name dictionary for customers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<IEnumerable<CustomerListView>>> GetListView()
        {
            var result = await _customers.GetCustomersListView();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CreatedCustomerView>> CreateCustomer([FromBody] CreateCustomerRequest req)
        {
            var result = await _customers.CreateCustomer(req);

            return Ok(_mapper.Map<CreatedCustomerView>(result));
        }

        [HttpDelete]
        [Route("{customerId}")]
        public async Task<ActionResult<bool>> DeleteCustomer(string customerId)
        {
            var result = await _customers.Remove(customerId);

            return result;
        }

        [HttpPatch]
        [Route("{customerId}")]
        public async Task<ActionResult<CustomerView>> UpdateCustomer([FromBody] UpdateCustomerRequest req, string customerId)
        {
            var updated = _mapper.Map<UpdateCustomerRequest, Customer>(req);

            updated.Id = customerId;

            var result = await _customers.Update(updated);

            return _mapper.Map<CustomerView>(result);
        }

        [HttpPost]
        [Route("{customerId}/demographics/{demographicId}")]
        public async Task<ActionResult<bool>> AddDemographicToCustomer(int demographicId, string customerId)
        {
            var result = await _customers.AddDemographicToCustomer(customerId, demographicId);

            return Ok(result);
        }

        [HttpDelete]
        [Route("{customerId}/demographics/{demographicId}")]
        public async Task<ActionResult<bool>> RemoveDemographicFromCustomer(int demographicId, string customerId)
        {
            var result = await _customers.RemoveDemographicFromCustomer(customerId, demographicId);

            return Ok(result);
        }
    }
}
