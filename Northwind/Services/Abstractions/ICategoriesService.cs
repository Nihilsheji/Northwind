using Northwind.Models.Entities;
using Northwind.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Services.Abstractions
{
    public interface ICategoriesService : ICrudServiceBase<Category>
    {
        Task<IEnumerable<DictionaryValue<int, string>>> GetDictionary();
    }
}
