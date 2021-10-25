using Northwind.DbContexts;
using Northwind.Models.Entities;
using Northwind.Models.Response;
using Northwind.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Services
{
    public class CategoriesService : CrudServiceBase<Category>, ICategoriesService
    {
        private readonly INorthwindDbContext _context;
        public CategoriesService(INorthwindDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DictionaryValue<int, string>>> GetDictionary()
        {
            return await GetDictionary((Category c) => new DictionaryValue<int, string>() { Key = c.Id, Value = c.Name });
        }
    }
}
