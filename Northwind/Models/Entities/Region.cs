using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Models.Entities
{
    public class Region
    {
        public int Id { get; set; }
        public string RegionDescription { get; set; }

        public IEnumerable<Territory> Territories { get; set; }        
    }
}
