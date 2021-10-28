using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Models.Response
{
    public class TerritoryView
    {
        public int Id { get; set; }
        public int RegionId { get; set; }
        public string Description { get; set; }
    }
}
