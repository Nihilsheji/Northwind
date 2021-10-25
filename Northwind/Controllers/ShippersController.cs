using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models.Entities;
using Northwind.Models.Request.Shipper;
using Northwind.Models.Response;
using Northwind.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Controllers
{
    [ApiController]
    [Route("shippers")]
    public class ShippersController : Controller
    {
        private readonly IShippersService _shippers;
        private readonly IMapper _mapper;

        public ShippersController(IShippersService shippers, IMapper mapper)
        {
            _shippers = shippers;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShipperView>>> Get()
        {
            var result = await _shippers.Get();

            return Ok(_mapper.Map<IEnumerable<ShipperView>>(result));
        }

        [HttpGet]
        [Route("{shipperId}")]
        public async Task<ActionResult<ShipperView>> Get(int shipperId)
        {
            var result = await _shippers.Get(shipperId);

            return Ok(_mapper.Map<ShipperView>(result));
        }

        [HttpPost]
        public async Task<ActionResult<ShipperView>> CreateShipper(CreateShipperRequest req)
        {
            var result = await _shippers.Create(_mapper.Map<CreateShipperRequest, Shipper>(req));

            return Ok(_mapper.Map<ShipperView>(result));
        }

        [HttpDelete]
        [Route("shipperId")]
        public async Task<ActionResult<bool>> DeleteShipper(int shipperId)
        {
            var result = await _shippers.Remove(shipperId);

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<ShipperView>> UpdateShipper(UpdateShipperRequest req)
        {
            var result = await _shippers.Update(_mapper.Map<UpdateShipperRequest, Shipper>(req));

            return Ok(_mapper.Map<ShipperView>(result));
        }
    }
}
