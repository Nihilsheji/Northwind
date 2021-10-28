using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Northwind.Models.Entities
{
    public class Order
    {
        public static Dictionary<string, LambdaExpression> PropDictionary { get; }
            = new Dictionary<string, LambdaExpression>()
            {
                { "orderDate", (Expression<Func<Order, DateTime?>>)((Order o) => o.OrderDate) },
                { "requiredDate", (Expression<Func<Order, DateTime?>>)((Order o) => o.RequiredDate) },
                { "shippedDate", (Expression<Func<Order, DateTime?>>)((Order o) => o.ShippedDate) },
                { "shipName", (Expression<Func<Order, string>>)((Order o) => o.ShipName) },
                { "shipAddress", (Expression<Func<Order, string>>)((Order o) => o.ShipAddress) },
                { "shipCity", (Expression<Func<Order, string>>)((Order o) => o.ShipRegion) },
                { "shipPostaCode", (Expression<Func<Order, string>>)((Order o) => o.ShipPostalCode) },
                { "shipCountry", (Expression<Func<Order, string>>)((Order o) => o.ShipCountry) }
            };

        public static Dictionary<string, PropertyInfo> PropInfoDictionary { get; set; }
            = new Dictionary<string, PropertyInfo>()
            {
                { "orderDate", typeof(Order).GetProperty("OrderDate") },
                { "requiredDate", typeof(Order).GetProperty("RequiredDate") },
                { "shippedDate", typeof(Order).GetProperty("ShippedDate") },
                { "shipName", typeof(Order).GetProperty("ShipName") },
                { "shipAddress", typeof(Order).GetProperty("ShipAddress") },
                { "shipCity", typeof(Order).GetProperty("ShipCity") },
                { "shipPostaCode", typeof(Order).GetProperty("ShipPostalCode") },
                { "shipCountry", typeof(Order).GetProperty("ShipCountry") }
            };


        public int Id { get; set; }
        public string CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int ShipViaId { get; set; }
        public decimal Freight { get; set; }
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
    }
}
