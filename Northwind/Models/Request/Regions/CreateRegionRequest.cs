using Northwind.Models.Request.Territory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Models.Request.Regions
{
    public class CreateRegionRequest
    {
        public string RegionDescription { get; set; }
        public IEnumerable<CreateTerritoryRequest> Territories { get; set; }
    }
}
