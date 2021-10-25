using Northwind.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Northwind.Models.Entities
{
    public class Order : IExpAccess<Order>
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int ShipViaId { get; set; }
        public float Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }


        public Customer Customer { get; set; }
        public Employee Employee { get; set; }
        public Shipper ShipVia { get; set; }
        public IEnumerable<OrderDetails> OrderDetails { get; set; }

        public Expression<Func<Order, int>> GetIntPropertyExp(string propName)
        {
            Expression<Func<Order, int>> res = (propName) switch
            {
                "id" => (Order o) => o.Id,
                "employeeId" => (Order o) => o.EmployeeId,
                "shipViaId" => (Order o) => o.ShipViaId,
                _ => null
            };

            return res;
        }

        public Expression<Func<Order, float>> GetFloatPropertyExp(string propName)
        {
            Expression<Func<Order, float>> res = (propName) switch
            {
                "freight" => (Order o) => o.Freight,
                _ => null
            };

            return res;
        }

        public Expression<Func<Order, DateTime?>> GetDateTimePropertyExp(string propName)
        {
            Expression<Func<Order, DateTime?>> res = (propName) switch
            {
                "orderDate" => (Order o) => o.OrderDate,
                "requiredDate" => (Order o) => o.RequiredDate,
                "shippedDate" => (Order o) => o.ShippedDate,
                _ => null
            };

            return res;
        }

        public Expression<Func<Order, string>> GetStringPropertyExp(string propName)
        {
            Expression<Func<Order, string>> res = (propName) switch
            {
                "customerId" => (Order o) => o.CustomerId,
                "shipName" => (Order o) => o.ShipName,
                "shipAddress" => (Order o) => o.ShipAddress,
                "shipCity" => (Order o) => o.ShipCity,
                "shipRegion" => (Order o) => o.ShipRegion,
                "shipPostalCode" => (Order o) => o.ShipPostalCode,
                "shipCountry" => (Order o) => o.ShipCountry,
                _ => null
            };

            return res;
        }

        public Order MakeEmpty()
        {
            return new Order();
        }

        public static Order GetInstance()
        {
            return new Order();
        }
    }
}
