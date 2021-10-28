using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Northwind.DbContexts.Queries;
using Northwind.Models.Entities;
using Northwind.Models.Request;
using Northwind.Models.Request.Category;
using Northwind.Models.Response;
using Northwind.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Controllers
{
    [ApiController]
    [Route("categories")]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService _categories;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoriesService categories, IMapper mapper)
        {
            _categories = categories;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryView>>> Get()
        {
            var result = await _categories.Get();

            return Ok(_mapper.Map<IEnumerable<CategoryView>>(result));
        }

        /// <summary>
        /// Gets id, name dictionary 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("dictionary")]
        public async Task<ActionResult<IEnumerable<DictionaryValue<int, string>>>> GetDictionary()
        {
            var result = await _categories.GetDictionary();

            return Ok(result);
        }

        /// <summary>
        /// Gets id, name dictionary 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{categoryId}")]
        public async Task<ActionResult<CategoryView>> GetCategoryDetails(int categoryId)
        {
            var result = await _categories.Get(categoryId);

            return Ok(_mapper.Map<CategoryView>(result));
        }


        /// <summary>
        /// Creates new category
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CategoryView>> PostCategory([FromBody] CreateCategoryRequest request)
        {
            var category = _mapper.Map<CreateCategoryRequest, Category>(request);

            var result = await _categories.Create(category);

            return Ok(_mapper.Map<CategoryView>(result));
        }

        /// <summary>
        /// Removes category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{categoryId}")]
        public async Task<ActionResult<bool>> RemoveCategory(int categoryId)
        {
            var result = await _categories.Remove(categoryId);

            return Ok(result);
        }

        /// <summary>
        /// Updates existing category
        /// </summary>
        /// <param name="request"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{categoryId}")]
        public async Task<ActionResult<CategoryView>> UpdateCategory([FromBody] UpdateCategoryRequest request, int categoryId)
        {
            var updated = _mapper.Map<Category>(request);

            updated.Id = categoryId;

            var result = await _categories.Update(updated);

            return Ok(_mapper.Map<CategoryView>(result));
        }

        /// <summary>
        /// Gets categories for complex requests(filtering, pagination, sorting, projection)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("filters")]
        public async Task<ActionResult<IEnumerable<CategoryView>>> GetCategories([FromBody] GetRequest request)
        {
            var result = await _categories.Get(new GetQueryOptions<Category>()
            {
                Count = request.ItemsPerPage,
                Skip = request.ItemsPerPage * (request.Page - 1),
                Filter = request.Filters?.GetExpression<Category>(),
                Sort = request.Sorting?.GetSortExpressions<Category>()
            }); ;

            return Ok(_mapper.Map<IEnumerable<CategoryView>>(result));
        }
    }
}
