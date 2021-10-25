using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Models.Entities
{
    public class Demographic
    {
        public int Id { get; set; }
        public string CustomerDesc { get; set; }

        public ICollection<Customer> Customers { get; set; }
    }
}
