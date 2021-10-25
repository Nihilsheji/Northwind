using Northwind.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Northwind.Models.Entities
{
    public class Category : IExpAccess<Category>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }

        public IEnumerable<Product> Products { get; set; }

        public Expression<Func<Category, int>> GetIntPropertyExp(string propName)
        {
            Expression<Func<Category, int>> res = (propName) switch
            {
                "id" => (Category x) => x.Id,
                _ => null
            };

            return res;
        }

        public Expression<Func<Category, string>> GetStringPropertyExp(string propName)
        {
            Expression<Func<Category, string>> res = (propName) switch
            {
                "Name" => (Category x) => x.Name,
                "Description" => (Category x) => x.Description,
                "Picture" => (Category x) => x.Picture,
                _ => null
            };

            return res;
        }

        public Category MakeEmpty()
        {
            return new Category();
        }

        public static Category GetInstance()
        {
            return new Category();
        }
    }
}
