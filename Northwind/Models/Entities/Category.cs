using Northwind.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Northwind.Models.Entities
{
    public class Category : IIdentificable<int>
    {
        public static Dictionary<string, LambdaExpression> PropDictionary { get; }
           = new Dictionary<string, LambdaExpression>()
           {
                { "name", (Expression<Func<Category, string>>)((Category c) => c.Name) },
                { "description", (Expression<Func<Category, string>>)((Category c) => c.Description)}
           };


        public static Dictionary<string, PropertyInfo> PropInfoDictionary { get; set; }
            = new Dictionary<string, PropertyInfo>()
            {
                { "name", typeof(Category).GetProperty("Name") },
                { "description", typeof(Category).GetProperty("Description") }               
            };

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
