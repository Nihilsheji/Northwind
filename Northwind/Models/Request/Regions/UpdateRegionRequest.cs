using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Models.Request.Regions
{
    public class UpdateRegionRequest
    {
        public string RegionDescription { get; set; }
        public IEnumerable<int> Territories { get; set; }
    }
}
