using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Models.Entities
{
    public class Territory
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int RegionId { get; set; }

        public Region Region { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
