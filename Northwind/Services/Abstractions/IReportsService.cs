using Northwind.Models.Request;
using Northwind.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Services.Abstractions
{
    public interface IReportsService
    {
        Task<IEnumerable<ProductReportView>> GetBestSellingProducts();
        Task<IEnumerable<EmployeeReportView>> GetBestSellingEmployees();
        Task<IEnumerable<CustomerReportView>> GetBestBuyingCustomers();
        Task<IEnumerable<ProductSalesView>> GetProductSaleStatistics(int productId, GetStatisticsRequest req);
        Task<IEnumerable<PopularityReportView>> GetMostPopularCategories();
    }
}
