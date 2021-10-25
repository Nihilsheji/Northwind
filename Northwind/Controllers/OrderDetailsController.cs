using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models.Entities;
using Northwind.Models.Request.OrderDetails;
using Northwind.Models.Response;
using Northwind.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Northwind.Controllers
{
    [ApiController]
    [Route("orders/{orderId}/details")]
    public class OrderDetailsController : Controller
    {
        private readonly IOrderDetailsService _details;
        private readonly IMapper _mapper;

        public OrderDetailsController(IOrderDetailsService details, IMapper mapper)
        {
            _details = details;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetailsListView>>> GetOrderDetailsListViewForOrder(int orderId)
        {
            var result = await _details.GetOrderDetailsListViewForOrder(orderId);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDetails>> CreateOrderDetails(CreateOrderDetailsRequest req)
        {
            var result = await _details.CreateOrderDetails(req);

            return Ok(result);
        }

        [HttpPatch]
        public async Task<ActionResult<OrderDetails>> UpdateOrderDetails(UpdateOrderDetailsRequest req)
        {
            var result = await _details.UpdateOrderDetails(req);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteOrderDetails(RemoveOrderDetailsRequest req)
        {
            var result = await _details.RemoveOrderDetails(req);

            return Ok(result);
        }
    }
}
