using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Northwind.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "varchar(15)", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Picture = table.Column<string>(type: "varchar(40)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<string>(type: "varchar(5)", nullable: false),
                    CompanyName = table.Column<string>(type: "varchar(40)", nullable: true),
                    ContactName = table.Column<string>(type: "varchar(30)", nullable: true),
                    ContactTitle = table.Column<string>(type: "varchar(30)", nullable: true),
                    Address = table.Column<string>(type: "varchar(60)", nullable: true),
                    City = table.Column<string>(type: "varchar(15)", nullable: true),
                    Region = table.Column<string>(type: "varchar(15)", nullable: true),
                    PostalCode = table.Column<string>(type: "varchar(10)", nullable: true),
                    Country = table.Column<string>(type: "varchar(15)", nullable: true),
                    Phone = table.Column<string>(type: "varchar(24)", nullable: true),
                    Fax = table.Column<string>(type: "varchar(24)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "Demographics",
                columns: table => new
                {
                    CustomerTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerDesc = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Demographics", x => x.CustomerTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(type: "varchar(20)", nullable: true),
                    FirstName = table.Column<string>(type: "varchar(10)", nullable: true),
                    Title = table.Column<string>(type: "varchar(30)", nullable: true),
                    TitleOfCourtesy = table.Column<string>(type: "varchar(25)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Address = table.Column<string>(type: "varchar(60)", nullable: true),
                    City = table.Column<string>(type: "varchar(15)", nullable: true),
                    Region = table.Column<string>(type: "varchar(15)", nullable: true),
                    PostalCode = table.Column<string>(type: "varchar(10)", nullable: true),
                    Country = table.Column<string>(type: "varchar(15)", nullable: true),
                    HomePhone = table.Column<string>(type: "varchar(24)", nullable: true),
                    Extension = table.Column<string>(type: "varchar(4)", nullable: true),
                    Photo = table.Column<string>(type: "varchar(40)", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportsTo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeID);
                    table.ForeignKey(
                        name: "FK_Employees_Employees_ReportsTo",
                        column: x => x.ReportsTo,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    RegionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.RegionID);
                });

            migrationBuilder.CreateTable(
                name: "Shippers",
                columns: table => new
                {
                    ShipperID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "varchar(40)", nullable: true),
                    Phone = table.Column<string>(type: "varchar(24)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shippers", x => x.ShipperID);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "varchar(40)", nullable: true),
                    ContactName = table.Column<string>(type: "varchar(30)", nullable: true),
                    ContactTitle = table.Column<string>(type: "varchar(30)", nullable: true),
                    Address = table.Column<string>(type: "varchar(60)", nullable: true),
                    City = table.Column<string>(type: "varchar(15)", nullable: true),
                    Region = table.Column<string>(type: "varchar(15)", nullable: true),
                    PostalCode = table.Column<string>(type: "varchar(10)", nullable: true),
                    Country = table.Column<string>(type: "varchar(15)", nullable: true),
                    Phone = table.Column<string>(type: "varchar(24)", nullable: true),
                    Fax = table.Column<string>(type: "varchar(24)", nullable: true),
                    HomePage = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierID);
                });

            migrationBuilder.CreateTable(
                name: "CustomerDemographic",
                columns: table => new
                {
                    CustomersId = table.Column<string>(type: "varchar(5)", nullable: false),
                    DemographicsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerDemographic", x => new { x.CustomersId, x.DemographicsId });
                    table.ForeignKey(
                        name: "FK_CustomerDemographic_Customers_CustomersId",
                        column: x => x.CustomersId,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerDemographic_Demographics_DemographicsId",
                        column: x => x.DemographicsId,
                        principalTable: "Demographics",
                        principalColumn: "CustomerTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Territories",
                columns: table => new
                {
                    TerritoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Territories", x => x.TerritoryID);
                    table.ForeignKey(
                        name: "FK_Territories_Regions_RegionID",
                        column: x => x.RegionID,
                        principalTable: "Regions",
                        principalColumn: "RegionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<string>(type: "varchar(5)", nullable: false),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RequiredDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShippedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShipVia = table.Column<int>(type: "int", nullable: false),
                    Freight = table.Column<float>(type: "float(1)", nullable: false, defaultValue: 0f),
                    ShipName = table.Column<string>(type: "varchar(40)", nullable: true),
                    ShipAddress = table.Column<string>(type: "varchar(60)", nullable: true),
                    ShipCity = table.Column<string>(type: "varchar(15)", nullable: true),
                    ShipRegion = table.Column<string>(type: "varchar(15)", nullable: true),
                    ShipPostalCode = table.Column<string>(type: "varchar(10)", nullable: true),
                    ShipCountry = table.Column<string>(type: "varchar(15)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Shippers_ShipVia",
                        column: x => x.ShipVia,
                        principalTable: "Shippers",
                        principalColumn: "ShipperID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "varchar(40)", nullable: true),
                    SupplierID = table.Column<int>(type: "int", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    QuantityPerUnit = table.Column<string>(type: "varchar(20)", nullable: false),
                    UnitPrice = table.Column<double>(type: "float(1)", nullable: false, defaultValue: 0.0),
                    UnitsInStock = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)0),
                    UnitsOnOrder = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)0),
                    ReorderLevel = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)0),
                    Discontinued = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeTerritory",
                columns: table => new
                {
                    EmployeesId = table.Column<int>(type: "int", nullable: false),
                    TerritoriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTerritory", x => new { x.EmployeesId, x.TerritoriesId });
                    table.ForeignKey(
                        name: "FK_EmployeeTerritory_Employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeTerritory_Territories_TerritoriesId",
                        column: x => x.TerritoriesId,
                        principalTable: "Territories",
                        principalColumn: "TerritoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    odID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ProductID = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    UnitPrice = table.Column<double>(type: "float(1)", nullable: false, defaultValue: 0.0),
                    Quantity = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)1),
                    Discount = table.Column<double>(type: "float(1)", nullable: false, defaultValue: 0.0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.odID);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDemographic_DemographicsId",
                table: "CustomerDemographic",
                column: "DemographicsId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ReportsTo",
                table: "Employees",
                column: "ReportsTo");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTerritory_TerritoriesId",
                table: "EmployeeTerritory",
                column: "TerritoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderID",
                table: "OrderDetails",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductID",
                table: "OrderDetails",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerID",
                table: "Orders",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EmployeeID",
                table: "Orders",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShipVia",
                table: "Orders",
                column: "ShipVia");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplierID",
                table: "Products",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_Territories_RegionID",
                table: "Territories",
                column: "RegionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerDemographic");

            migrationBuilder.DropTable(
                name: "EmployeeTerritory");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Demographics");

            migrationBuilder.DropTable(
                name: "Territories");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Shippers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
