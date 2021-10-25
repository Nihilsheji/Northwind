using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Northwind.DbContexts.Queries;
using Northwind.Models.Entities;
using Northwind.Models.Products;
using Northwind.Models.Response;
using Northwind.Services.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductsController : Controller
    {
        private readonly IProductsService _products;
        public readonly IMapper _mapper;

        public ProductsController(IProductsService products, IMapper mapper)
        {
            _products = products;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductView>>> GetAllProducts()
        {
            var result = await _products.Get();

            return Ok(_mapper.Map<IEnumerable<ProductView>>(result));
        }

        [HttpGet]
        [Route("dictionary")]
        public async Task<ActionResult<IEnumerable<DictionaryValue<int, string>>>> GetDictionary()
        {
            var result = await _products.GetDictionary();

            return Ok(result);
        }

        [HttpGet]
        [Route("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<ProductListView>>> GetProductsForCategory(int categoryId)
        {
            var result = await _products.GetProductsListViewForCategory(categoryId);

            return Ok(result);
        }

        [HttpGet]
        [Route("supplier/{suppliersId}")]
        public async Task<ActionResult<IEnumerable<ProductListView>>> GetProductsForSupplier(int supplierId)
        {
            var result = await _products.GetProductsListViewForSupplier(supplierId);

            return Ok(result);
        }

        //Optional post for discontinue

        [HttpPost]
        public async Task<ActionResult<ProductView>> CreateProduct([FromBody] CreateProductRequest req)
        {
            var result = await _products.Create(_mapper.Map<CreateProductRequest, Product>(req));

            return Ok(_mapper.Map<ProductView>(result));
        }

        [HttpDelete]
        [Route("{productId}")]
        public async Task<ActionResult<bool>> DeleteeProduct(int productId)
        {
            var result = await _products.Remove(productId);

            return result;
        }

        [HttpPut]
        [Route("{productId}")]
        public async Task<ActionResult<ProductView>> UpdateProduct([FromBody] UpdateProductRequest req)
        {
            var result = await _products.Create(_mapper.Map<UpdateProductRequest, Product>(req));

            return Ok(_mapper.Map<ProductView>(result));
        }

        [HttpPost]
        [Route("stock")]
        public async Task<ActionResult<IEnumerable<ProductStockView>>> GetStocks([FromBody] IEnumerable<int> ids) {
            var result = await _products.Get(new GetQueryOptions<Product, ProductStockView>()
            {
                Filter = (Product p) => ids.Contains(p.Id),
                Select = (IQueryable<Product> q) => 
                    q.Select((Product p) => new ProductStockView() { Id = p.Id, Stock = p.UnitsInStock })
            });

            return Ok(result);
        }

        [HttpGet]
        [Route("{productId}/stock")]
        public async Task<ActionResult<ProductStockView>> GetStock(int productId)
        {
            var result = await _products.Get(new GetSingleQueryOptions<Product, ProductStockView>()
            {
                Filter = (Product p) => p.Id == productId,
                Select = (IQueryable<Product> q) =>
                    q.Select((Product p) => new ProductStockView() { Id = p.Id, Stock = p.UnitsInStock })
            });

            return Ok(result);
        }
    }
}
