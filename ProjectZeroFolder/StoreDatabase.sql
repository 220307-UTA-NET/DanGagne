CREATE SCHEMA Store;
GO

CREATE TABLE Store.Location(
    StoreLocationID INT IDENTITY PRIMARY KEY,
    StoreLocationName NVARCHAR(100) NOT NULL,
    StoreAddress NVARCHAR(100) NOT NULL,
    StoreCity NVARCHAR(100) NOT NULL,
    StoreState CHAR(2) NOT NULL,
    StoreCountry CHAR (3) NOT NULL
);
--DROP TABLE Store.Location

CREATE TABLE Store.Stock(
    ItemName NVARCHAR(1000) NOT NULL,
    Price DECIMAL(19,2) NOT NULL,
    Quantity INT,
    Description NVARCHAR(255) NOT NULL,
    Material NVARCHAR(255) NOT NULL,
    SKU INT NOT NULL,
    StoreLocationID INT NOT NULL
        CONSTRAINT FK_Location_StoreLocation FOREIGN KEY(StoreLocationID)
        REFERENCES Store.Location(StoreLocationID),
    PRIMARY KEY(SKU, StoreLocationID)
);
--DROP TABLE Store.Stock

CREATE TABLE Store.Customer(
    NameFirst NVARCHAR(100) NOT NULL,
    NameLast NVARCHAR(100) NOT NULL,
    Address NVARCHAR(100),
    City NVARCHAR(100),
    State CHAR(2),
    Country CHAR (3),
    DOB DATE NULL,
    CustomerID INT IDENTITY(100,1) PRIMARY KEY,
    DefaultStoreLocation INT NOT NULL
        CONSTRAINT FK_Location_StoreLocationID FOREIGN KEY(DefaultStoreLocation)
        REFERENCES Store.Location(StoreLocationID),
);
--DROP TABLE Store.Customer


CREATE TABLE Store.Purchase(
    CustomerID INT NOT NULL
        CONSTRAINT FK_Customer_CustomerID FOREIGN KEY (CustomerID)
        REFERENCES Store.Customer(CustomerID),
    StoreLocationID INT NOT NULL,
    SKU INT NOT NULL,
        CONSTRAINT FK_Stock_SKU_LocationID FOREIGN KEY (SKU, StoreLocationID)
        REFERENCES Store.Stock(SKU, StoreLocationID),
    Quantity INT NOT NULL,
    PurchaseDate NVARCHAR (255)NOT NULL, 
    PurchaseNumber INT IDENTITY PRIMARY KEY
);
--DROP TABLE Store.Purchase

CREATE TABLE Store.ShoppingCart(
    CustomerID INT NOT NULL
    CONSTRAINT FK_SC_Customer_CustomerID FOREIGN KEY (CustomerID)
        REFERENCES Store.Customer(CustomerID),
    StoreLocationID INT NOT NULL,
    SKU INT NOT NULL,
        CONSTRAINT FK_SC_Stock_SKU_LocationID FOREIGN KEY (SKU, StoreLocationID)
        REFERENCES Store.Stock(SKU, StoreLocationID),
    Quantity INT NOT NULL,
    
)
--DROP TABLE Store.ShoppingCart

SELECT * FROM Store.Location;
SELECT * FROM Store.Stock 
SELECT * FROM Store.Customer;
SELECT * FROM Store.Purchase;
SELECT * FROM Store.ShoppingCart
DELETE FROM Store.ShoppingCart


--INNER JOIN Store.Stock ON Store.ShoppingCart.SKU=Store.Stock.SKU  WHERE Store.ShoppingCart.CustomerID=103 AND Store.Stock.StoreLocationID=1
INSERT INTO Store.Purchase(CustomerID, StoreLocationID, SKU, Quantity)
VALUES(100, 2, 10004,2)


--UPDATE CUSTOMER
INSERT INTO Store.Customer(NameFirst, NameLast, DefaultStoreLocation)
VALUES
 ('test1', 'test2',2),
 ('test1', 'test2',2),
 ('test1', 'test2',2),
 ('test1', 'test2',2)

INSERT INTO Store.Location(StoreLocationName, StoreAddress, StoreCity, StoreState, StoreCountry)
VALUES
    ('Rochester Branch', '45 East Ave', 'Rochester', 'NY','USA'),
    ('Buffalo Branch', '275 Allen Street', 'Buffalo', 'NY','USA'),
    ('Syracuse Branch', '98 Perkins Lane', 'Syracuse', 'NY','USA')

INSERT INTO Store.Stock(ItemName,Price, Quantity, [Description], Material, SKU, StoreLocationID)
VALUES
    ('Lamp', 250.00, 10, 'Desk Lamp', 'Mahogany', 10005, 1),
    ('Lamp', 250.00, 15, 'Desk Lamp', 'Mahogany', 10005, 2),
    ('Lamp', 250.00, 12, 'Desk Lamp', 'Mahogany', 10005, 3),
    ('Table', 500.00, 8, 'Dining Table', 'Oak', 10006, 1),
    ('Table', 500.00, 7, 'Dining Table', 'Oak', 10006, 2),
    ('Table', 500.00, 13, 'Dining Table', 'Oak', 10006, 3),
    ('Chair', 600.00, 4, 'Corner Chair', 'Oak', 10007, 1),
    ('Chair', 600.00, 10, 'Corner Chair', 'Oak', 10007, 2),
    ('Chair', 600.00, 8, 'Corner Chair', 'Oak', 10007, 3),
    ('Side Table', 150.00, 15, 'Side Table', 'Metal', 10008, 1),
    ('Side Table', 150.00, 18, 'Side Table', 'Metal', 10008, 2),
    ('Side Table', 150.00, 20, 'Side Table', 'Metal', 10008, 3)

INSERT INTO Store.Customer(NameFirst,NameLast, Address, City, [State], Country, DOB, DefaultStoreLocation)
VALUES
    ('Luke', 'Skywalker', '75 Dune Lane', 'Chicago', 'IL', 'USA', '1985-05-12', 2),
    ('Han', 'Solo', '81 Corellia Ave', 'Brooklyn', 'NY', 'USA', '1981-07-19', 3),
    ('Leia', 'Organa', '111  Alderaan Way', 'San Francisco', 'CA', 'USA', '1985-05-12',1)

UPDATE Store.Stock
SET Quantity=2
WHERE  StoreLocationID=1 AND SKU=10001

SELECT * FROM Store.Stock

SELECT * FROM Store.Purchase
INNER JOIN Store.Stock ON Store.Stock.SKU=Store.Purchase.SKU AND store.Stock.StoreLocationID = store.purchase.StoreLocationID WHERE CustomerID=100;

SELECT * FROM Store.Purchase
WHERE CustomerID=100

SELECT * FROM Store.Purchase INNER JOIN Store.Stock ON (Store.Stock.SKU=Store.Purchase.SKU AND Store.Stock.StoreLocationID=Store.Purchase.StoreLocationID) WHERE Purchase.StoreLocationID=1;

