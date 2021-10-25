using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models.Entities;
using Northwind.Models.Request;
using Northwind.Models.Request.Order;
using Northwind.Models.Response;
using Northwind.Services.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northwind.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrdersController : Controller
    {
        private readonly IOrdersService _orders;
        private readonly IMapper _mapper;

        public OrdersController(IOrdersService orders, IMapper mapper)
        {
            _orders = orders;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderView>>> GetAllOrders()
        {
            var result = await _orders.Get();

            return Ok(_mapper.Map<IEnumerable<OrderView>>(result));
        }

        [HttpGet]
        [Route("{orderId}")]
        public async Task<ActionResult<OrderView>> GetOrder(int orderId)
        {
            var result = await _orders.Get(orderId);

            return Ok(_mapper.Map<OrderView>(result));
        }

        [HttpGet]
        [Route("employee/{employeeId}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersListViewForEmployee(int employeeId)
        {
            var result = await _orders.GetOrdersListViewForEmployee(employeeId);

            return Ok(result);
        }

        [HttpGet]
        [Route("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersListViewForCustomer(string customerId)
        {
            var result = await _orders.GetOrdersListViewForCustomer(customerId);

            return Ok(result);
        }

        [HttpGet]
        [Route("shipper/{shipperId}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersListViewForShipper(int shipperId)
        {
            var result = await _orders.GetOrdersListViewForShipper(shipperId);

            return Ok(result);
        }

        [HttpGet]
        [Route("product/{productId}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersListViewForProduct(int productId)
        {
            var result = await _orders.GetOrdersListViewForProduct(productId);

            return Ok(result);
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<IEnumerable<OrderListView>>> GetOrdersListView()
        {
            var result = await _orders.GetOrdersListView();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CreatedOrderView>> CreateOrder([FromBody] CreateOrderRequest req)
        {
            var result = await _orders.CreateOrder(req);

            return Ok(result);
        }

        [HttpDelete]
        [Route("{orderId}")]
        public async Task<ActionResult<bool>> DeleteOrder(int orderId)
        {
            var result = await _orders.RemoveOrder(orderId);

            return Ok(result);
        }

        [HttpPatch]
        [Route("{orderId}")]
        public async Task<ActionResult<OrderView>> UpdateOrder([FromBody] UpdateOrderRequest req)
        {
            var result = await _orders.Create(_mapper.Map<UpdateOrderRequest, Order>(req));

            return Ok(_mapper.Map<OrderView>(result));
        }

        [HttpPost]
        [Route("filter")]
        public async Task<ActionResult<IEnumerable<OrderListView>>> GetFilteredOrdersListView([FromBody] GetRequest req) 
        {
            var result = await _orders.GetFilteredOrdersListView(req);

            return Ok(result);
        }
    }
}
