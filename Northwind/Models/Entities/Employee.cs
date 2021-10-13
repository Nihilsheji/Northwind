using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Models.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public string TitleOfCourtesy { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime HireDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string HomePhone { get; set; }
        public string Extension { get; set; }
        public byte[] Photo { get; set; } //? File/Blob
        public string Notes { get; set; }
        public string PhotoPath { get; set; }
        public int ReportsToId { get; set; }

        public Employee ReportsTo { get; set; }

        public IEnumerable<Employee> Subordinates { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<Territory> Territories { get; set; }
    }
}
