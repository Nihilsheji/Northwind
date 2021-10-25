IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Categories] (
    [CategoryID] int NOT NULL IDENTITY,
    [CategoryName] varchar(15) NULL,
    [Description] text NULL,
    [Picture] varchar(40) NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([CategoryID])
);
GO

CREATE TABLE [Customers] (
    [CustomerID] varchar(5) NOT NULL,
    [CompanyName] varchar(40) NULL,
    [ContactName] varchar(30) NULL,
    [ContactTitle] varchar(30) NULL,
    [Address] varchar(60) NULL,
    [City] varchar(15) NULL,
    [Region] varchar(15) NULL,
    [PostalCode] varchar(10) NULL,
    [Country] varchar(15) NULL,
    [Phone] varchar(24) NULL,
    [Fax] varchar(24) NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY ([CustomerID])
);
GO

CREATE TABLE [Demographics] (
    [CustomerTypeID] int NOT NULL IDENTITY,
    [CustomerDesc] nvarchar(max) NULL,
    CONSTRAINT [PK_Demographics] PRIMARY KEY ([CustomerTypeID])
);
GO

CREATE TABLE [Employees] (
    [EmployeeID] int NOT NULL IDENTITY,
    [LastName] varchar(20) NULL,
    [FirstName] varchar(10) NULL,
    [Title] varchar(30) NULL,
    [TitleOfCourtesy] varchar(25) NULL,
    [BirthDate] datetime2 NULL,
    [HireDate] datetime2 NULL,
    [Address] varchar(60) NULL,
    [City] varchar(15) NULL,
    [Region] varchar(15) NULL,
    [PostalCode] varchar(10) NULL,
    [Country] varchar(15) NULL,
    [HomePhone] varchar(24) NULL,
    [Extension] varchar(4) NULL,
    [Photo] varchar(40) NULL,
    [Notes] text NULL,
    [PhotoPath] nvarchar(max) NULL,
    [ReportsTo] int NOT NULL,
    CONSTRAINT [PK_Employees] PRIMARY KEY ([EmployeeID]),
    CONSTRAINT [FK_Employees_Employees_ReportsTo] FOREIGN KEY ([ReportsTo]) REFERENCES [Employees] ([EmployeeID]) ON DELETE CASCADE
);
GO

CREATE TABLE [Regions] (
    [RegionID] int NOT NULL IDENTITY,
    [RegionDescription] nvarchar(max) NULL,
    CONSTRAINT [PK_Regions] PRIMARY KEY ([RegionID])
);
GO

CREATE TABLE [Shippers] (
    [ShipperID] int NOT NULL IDENTITY,
    [CompanyName] varchar(40) NULL,
    [Phone] varchar(24) NULL,
    CONSTRAINT [PK_Shippers] PRIMARY KEY ([ShipperID])
);
GO

CREATE TABLE [Suppliers] (
    [SupplierID] int NOT NULL IDENTITY,
    [CompanyName] varchar(40) NULL,
    [ContactName] varchar(30) NULL,
    [ContactTitle] varchar(30) NULL,
    [Address] varchar(60) NULL,
    [City] varchar(15) NULL,
    [Region] varchar(15) NULL,
    [PostalCode] varchar(10) NULL,
    [Country] varchar(15) NULL,
    [Phone] varchar(24) NULL,
    [Fax] varchar(24) NULL,
    [HomePage] text NULL,
    CONSTRAINT [PK_Suppliers] PRIMARY KEY ([SupplierID])
);
GO

CREATE TABLE [CustomerDemographic] (
    [CustomersId] varchar(5) NOT NULL,
    [DemographicsId] int NOT NULL,
    CONSTRAINT [PK_CustomerDemographic] PRIMARY KEY ([CustomersId], [DemographicsId]),
    CONSTRAINT [FK_CustomerDemographic_Customers_CustomersId] FOREIGN KEY ([CustomersId]) REFERENCES [Customers] ([CustomerID]) ON DELETE CASCADE,
    CONSTRAINT [FK_CustomerDemographic_Demographics_DemographicsId] FOREIGN KEY ([DemographicsId]) REFERENCES [Demographics] ([CustomerTypeID]) ON DELETE CASCADE
);
GO

CREATE TABLE [Territories] (
    [TerritoryID] int NOT NULL IDENTITY,
    [Description] nvarchar(max) NULL,
    [RegionID] int NOT NULL,
    CONSTRAINT [PK_Territories] PRIMARY KEY ([TerritoryID]),
    CONSTRAINT [FK_Territories_Regions_RegionID] FOREIGN KEY ([RegionID]) REFERENCES [Regions] ([RegionID]) ON DELETE CASCADE
);
GO

CREATE TABLE [Orders] (
    [OrderID] int NOT NULL IDENTITY,
    [CustomerID] varchar(5) NOT NULL,
    [EmployeeID] int NOT NULL,
    [OrderDate] datetime2 NULL,
    [RequiredDate] datetime2 NULL,
    [ShippedDate] datetime2 NULL,
    [ShipVia] int NOT NULL,
    [Freight] float(1) NOT NULL DEFAULT CAST(0 AS float(1)),
    [ShipName] varchar(40) NULL,
    [ShipAddress] varchar(60) NULL,
    [ShipCity] varchar(15) NULL,
    [ShipRegion] varchar(15) NULL,
    [ShipPostalCode] varchar(10) NULL,
    [ShipCountry] varchar(15) NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([OrderID]),
    CONSTRAINT [FK_Orders_Customers_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [Customers] ([CustomerID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Orders_Employees_EmployeeID] FOREIGN KEY ([EmployeeID]) REFERENCES [Employees] ([EmployeeID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Orders_Shippers_ShipVia] FOREIGN KEY ([ShipVia]) REFERENCES [Shippers] ([ShipperID]) ON DELETE CASCADE
);
GO

CREATE TABLE [Products] (
    [ProductID] int NOT NULL IDENTITY,
    [ProductName] varchar(40) NULL,
    [SupplierID] int NOT NULL,
    [CategoryID] int NOT NULL,
    [QuantityPerUnit] varchar(20) NOT NULL,
    [UnitPrice] float(1) NOT NULL DEFAULT 0.0E0,
    [UnitsInStock] smallint NOT NULL DEFAULT CAST(0 AS smallint),
    [UnitsOnOrder] smallint NOT NULL DEFAULT CAST(0 AS smallint),
    [ReorderLevel] smallint NOT NULL DEFAULT CAST(0 AS smallint),
    [Discontinued] tinyint NOT NULL DEFAULT CAST(0 AS tinyint),
    CONSTRAINT [PK_Products] PRIMARY KEY ([ProductID]),
    CONSTRAINT [FK_Products_Categories_CategoryID] FOREIGN KEY ([CategoryID]) REFERENCES [Categories] ([CategoryID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Products_Suppliers_SupplierID] FOREIGN KEY ([SupplierID]) REFERENCES [Suppliers] ([SupplierID]) ON DELETE CASCADE
);
GO

CREATE TABLE [EmployeeTerritory] (
    [EmployeesId] int NOT NULL,
    [TerritoriesId] int NOT NULL,
    CONSTRAINT [PK_EmployeeTerritory] PRIMARY KEY ([EmployeesId], [TerritoriesId]),
    CONSTRAINT [FK_EmployeeTerritory_Employees_EmployeesId] FOREIGN KEY ([EmployeesId]) REFERENCES [Employees] ([EmployeeID]) ON DELETE CASCADE,
    CONSTRAINT [FK_EmployeeTerritory_Territories_TerritoriesId] FOREIGN KEY ([TerritoriesId]) REFERENCES [Territories] ([TerritoryID]) ON DELETE CASCADE
);
GO

CREATE TABLE [OrderDetails] (
    [odID] int NOT NULL IDENTITY,
    [OrderID] int NOT NULL DEFAULT 0,
    [ProductID] int NOT NULL DEFAULT 0,
    [UnitPrice] float(1) NOT NULL DEFAULT 0.0E0,
    [Quantity] smallint NOT NULL DEFAULT CAST(1 AS smallint),
    [Discount] float(1) NOT NULL DEFAULT 0.0E0,
    CONSTRAINT [PK_OrderDetails] PRIMARY KEY ([odID]),
    CONSTRAINT [FK_OrderDetails_Orders_OrderID] FOREIGN KEY ([OrderID]) REFERENCES [Orders] ([OrderID]) ON DELETE CASCADE,
    CONSTRAINT [FK_OrderDetails_Products_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [Products] ([ProductID]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_CustomerDemographic_DemographicsId] ON [CustomerDemographic] ([DemographicsId]);
GO

CREATE INDEX [IX_Employees_ReportsTo] ON [Employees] ([ReportsTo]);
GO

CREATE INDEX [IX_EmployeeTerritory_TerritoriesId] ON [EmployeeTerritory] ([TerritoriesId]);
GO

CREATE INDEX [IX_OrderDetails_OrderID] ON [OrderDetails] ([OrderID]);
GO

CREATE INDEX [IX_OrderDetails_ProductID] ON [OrderDetails] ([ProductID]);
GO

CREATE INDEX [IX_Orders_CustomerID] ON [Orders] ([CustomerID]);
GO

CREATE INDEX [IX_Orders_EmployeeID] ON [Orders] ([EmployeeID]);
GO

CREATE INDEX [IX_Orders_ShipVia] ON [Orders] ([ShipVia]);
GO

CREATE INDEX [IX_Products_CategoryID] ON [Products] ([CategoryID]);
GO

CREATE INDEX [IX_Products_SupplierID] ON [Products] ([SupplierID]);
GO

CREATE INDEX [IX_Territories_RegionID] ON [Territories] ([RegionID]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211015184150_Init', N'5.0.11');
GO

COMMIT;
GO

