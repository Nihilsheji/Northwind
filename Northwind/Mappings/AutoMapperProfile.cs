using AutoMapper;
using Northwind.Models.Entities;
using Northwind.Models.Products;
using Northwind.Models.Request;
using Northwind.Models.Request.Category;
using Northwind.Models.Request.Customer;
using Northwind.Models.Request.Demographic;
using Northwind.Models.Request.Employee;
using Northwind.Models.Request.Order;
using Northwind.Models.Request.OrderDetails;
using Northwind.Models.Request.Regions;
using Northwind.Models.Request.Shipper;
using Northwind.Models.Request.Suppliers;
using Northwind.Models.Request.Territory;
using Northwind.Models.Response;

namespace Northwind.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateCategoryRequest, Category>();
            CreateMap<UpdateCategoryRequest, Category>();
            CreateMap<Category, CategoryView>();

            CreateMap<CreateDemographicRequest, Demographic>();
            CreateMap<UpdateDemographicRequest, Demographic>();
            CreateMap<Demographic, DemographicView>();

            CreateMap<CreateCustomerRequest, Customer>();
            CreateMap<UpdateDemographicRequest, Customer>();
            CreateMap<Customer, CustomerListView>();
            CreateMap<Customer, CustomerView>();
            CreateMap<Customer, CreatedCustomerView>();

            CreateMap<CreateSupplierRequest, Supplier>();
            CreateMap<UpdateSupplierRequest, Supplier>();
            CreateMap<Supplier, SupplierListView>();
            CreateMap<Supplier, SupplierView>();

            CreateMap<CreateProductRequest, Product>().ForMember(x => x.Discontinued, opt => opt.MapFrom(x => false));
            CreateMap<UpdateProductRequest, Product>();
            CreateMap<Product, ProductView>();
            CreateMap<Product, ProductListView>();
            CreateMap<Product, ProductStockView>();

            CreateMap<CreateShipperRequest, Shipper>();
            CreateMap<UpdateShipperRequest, Shipper>();
            CreateMap<Shipper, ShipperView>();

            CreateMap<CreateRegionRequest, Region>();
            CreateMap<UpdateRegionRequest, Region>();
            CreateMap<Region, RegionView>();
            CreateMap<Region, CreatedRegionView>();

            CreateMap<CreateTerritoryRequest, Territory>();
            CreateMap<UpdateTerritoryRequest, Territory>();
            CreateMap<Territory, TerritoryView>();

            CreateMap<CreateEmployeeRequest, Employee>();
            CreateMap<UpdateEmployeeRequest, Employee>();
            CreateMap<Employee, EmployeeView>();
            CreateMap<Employee, EmployeeListView>();

            CreateMap<CreateOrderRequest, Order>();
            CreateMap<UpdateOrderRequest, Order>();
            CreateMap<Order, OrderListView>();
            CreateMap<Order, OrderView>();
            CreateMap<Order, CreatedOrderView>();

            CreateMap<CreateOrderDetailsRequest, OrderDetails>();
            CreateMap<UpdateOrderDetailsRequest, OrderDetails>();
            CreateMap<OrderDetails, OrderDetailsListView>();
            CreateMap<OrderDetails, OrderDetailsView>();
            CreateMap<OrderListViewBase, OrderListView>();
        }
    }
}
