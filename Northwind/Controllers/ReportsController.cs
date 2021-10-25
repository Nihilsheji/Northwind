using Microsoft.AspNetCore.Mvc;
using Northwind.Models.Request;
using Northwind.Models.Response;
using Northwind.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Controllers
{
    [ApiController]
    [Route("reports")]
    public class ReportsController : Controller
    {
        public readonly IReportsService _reports;

        public ReportsController(IReportsService reports)
        {
            _reports = reports;
        }
        

        [HttpGet]
        [Route("customers/best")]
        public async Task<ActionResult<IEnumerable<CustomerReportView>>> GetBestBuyingCustomers()
        {
            var result = await _reports.GetBestBuyingCustomers();

            return Ok(result);
        }

        [HttpGet]
        [Route("products/best")]
        public async Task<ActionResult<IEnumerable<ProductReportView>>> GetBestSellingProducts()
        {
            var result = await _reports.GetBestSellingProducts();

            return Ok(result);
        }

        [HttpGet]
        [Route("employees/best")]
        public async Task<ActionResult<IEnumerable<EmployeeReportView>>> GetBestSellingEmployees()
        {
            var result = await _reports.GetBestSellingEmployees();

            return Ok(result);
        }

        [HttpPost]
        [Route("products/{productId}/statistics")]
        public async Task<ActionResult<IEnumerable<ProductSalesView>>> GetSaleStatisticsForProduct(int productId, [FromBody] GetStatisticsRequest req)
        {
            var result = await _reports.GetProductSaleStatistics(productId, new GetStatisticsRequest() { Span = StatisticsSpan.Day });

            return Ok(result);
        }

        [HttpPost]
        [Route("categories/popular")]
        public async Task<ActionResult<IEnumerable<PopularityReportView>>> GetMostPopularCategories([FromBody] GetStatisticsRequest req)
        {
            var result = await _reports.GetMostPopularCategories();

            return Ok(result);
        }

    }
}
