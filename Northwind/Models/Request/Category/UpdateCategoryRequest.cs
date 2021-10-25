using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Models.Request.Category
{
    public class UpdateCategoryRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; } //TODO Change to byte[]
    }
}
