using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models.Entities;
using Northwind.Models.Request.Suppliers;
using Northwind.Models.Response;
using Northwind.Services;
using Northwind.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Controllers
{
    [ApiController]
    [Route("suppliers")]
    public class SuppliersController : Controller
    {
        private readonly ISuppliersService _suppliers;
        private readonly IMapper _mapper;

        public SuppliersController(ISuppliersService suppliers, IMapper mapper)
        {
            _suppliers = suppliers;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierView>>> GetAllSuppliers() {
            var result = await _suppliers.Get();

            return Ok(_mapper.Map<IEnumerable<SupplierView>>(result));
        }

        [HttpGet]
        [Route("dictionary")]
        public async Task<ActionResult<IEnumerable<DictionaryValue<int, string>>>> GetSuppliersDictionary()
        {
            var result = await _suppliers.GetDictionary();
            return Ok(result);
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<SupplierListView>> GetSuppliersListView()
        {
            var result = await _suppliers.GetSuppliersListView();
         
            return Ok(result);
        }


        [HttpGet]
        [Route("{supplierId}")]
        public async Task<ActionResult<SupplierView>> GetSupplier(int supplierId)
        {
            var result = await _suppliers.Get(supplierId);

            return Ok(_mapper.Map<SupplierView>(result));
        }

        [HttpPost]
        public async Task<ActionResult<SupplierView>> CreateSupplier([FromBody] CreateSupplierRequest req)
        {
            var result = await _suppliers.Create(_mapper.Map<CreateSupplierRequest, Supplier>(req));

            return _mapper.Map<SupplierView>(result);
        }

        [HttpDelete]
        [Route("{supplierId}")]
        public async Task<ActionResult<bool>> DeleteSupplier(int supplierId)
        {
            var result = await _suppliers.Remove(supplierId);

            return result;
        }

        [HttpPut]
        [Route("{supplierId}")]
        public async Task<ActionResult<SupplierView>> UpdateSupplier([FromBody] UpdateSupplierRequest req, int supplierId)
        {
            var updated = _mapper.Map<UpdateSupplierRequest, Supplier>(req);

            updated.Id = supplierId;

            var result = await _suppliers.Update(updated);

            return _mapper.Map<SupplierView>(result);
        }
    }
}
