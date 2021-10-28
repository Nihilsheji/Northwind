using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Models.Response
{
    public class CreatedRegionView
    {
        public int Id { get; set; }
        public string RegionDescription { get; set; }
        public IEnumerable<TerritoryView> Territories { get; set; }
    }
}
