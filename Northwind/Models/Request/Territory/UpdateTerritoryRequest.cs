using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Models.Request.Territory
{
    public class UpdateTerritoryRequest
    {
        public string TerritoryDescription { get; set; }
        public int RegionId { get; set; }
    }
}
