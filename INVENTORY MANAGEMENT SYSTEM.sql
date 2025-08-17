Create Database IMS 
USE IMS
create table Users(
UserId int primary key,
FullName varchar(100),
Phone int unique,
Email varchar(30) unique,
UserName varchar(20) unique,
Password varchar(20),
ConfirmPassword varchar(20),
status varchar(10),
DataRegister date,
Photo image
);


select * from Users

DROP DATABASE IMS;

------------------- ma save no


CREATE TABLE Customers (
    CustomerId INT IDENTITY(1,1) PRIMARY KEY,
    CustomerName VARCHAR(100),
    CustomerType VARCHAR(50),
    Phone VARCHAR(20),
    Gender VARCHAR(10),
    Dor DATE
);


CREATE TABLE Suppliers (
    SupplierID INT IDENTITY(1,1) PRIMARY KEY,
    SupplierName VARCHAR(100),
    PhoneNumber VARCHAR(20),
    Email VARCHAR(100),
    Address VARCHAR(255),
    CreatedDate DATETIME
);



select * from Suppliers



select * from Sales




-- Create the Category Table

CREATE TABLE Category (
    CategoryId INT PRIMARY KEY, 
    CategoryName VARCHAR(20)       
);
select * from Sales;




CREATE TABLE Products (
    ProductID INT IDENTITY(1,1) PRIMARY KEY,     
    CategoryName NVARCHAR(100),             
    ProductName NVARCHAR(100) NOT NULL,     
    Quantity INT NOT NULL CHECK (Quantity >= 0), 
    UnitPrice DECIMAL(18,2) NOT NULL CHECK (UnitPrice >= 0),
    Status NVARCHAR(50),                      
    DateAdded DATETIME DEFAULT GETDATE(),    
    SupplierID INT,                         
    FOREIGN KEY (SupplierID) REFERENCES Suppliers(SupplierID) -- Ensures referential integrity
);


select * from Products

CREATE TABLE Sales (
    SaleID INT IDENTITY(1,1) PRIMARY KEY,       -- Auto-incrementing Sale ID
    ProductID INT NOT NULL,                    -- Foreign key to Products table
    Quantity INT NOT NULL,                     -- Quantity of the product sold
    TotalAmount DECIMAL(18, 2) NOT NULL,       -- Total sale amount (Quantity * UnitPrice)
    SaleDate DATETIME NOT NULL DEFAULT GETDATE(), -- Sale date with default value as current date
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID) -- Establish foreign key relationship
);





select * from Products




drop table Products;

SELECT * 
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_NAME = 'Products';


drop database shopDb;


select * from Products


CREATE TABLE AddProducts (
    AddProductId INT PRIMARY KEY,           -- Unique identifier for each added product
    ProductID INT NOT NULL,                 -- Foreign key referencing Products table
    QuantityAdded INT NOT NULL,             -- Quantity of the product added
    AddedDate DATE NOT NULL,                -- Date the product was added
    CONSTRAINT FK_AddProducts_Products FOREIGN KEY (ProductID) REFERENCES Products(ProductID)

);

drop table Sales



select 0 as UserId, '---- Select User Name----' as UserName from Users Union Select UserId, UserName from Users

SELECT ISNULL(MAX(Userid), 0) + 1 FROM Users;

SELECT 0 as CategoryId,'---Select Category---' as  CategoryName FROM Category Union SELECT CategoryId, CategoryName FROM Catego