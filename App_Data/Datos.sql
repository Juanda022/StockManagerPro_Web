-- Employee Table
INSERT INTO [dbo].[Employee] ([EmployeeID], [Name], [Age], [Position], [UserID])
VALUES
(1, 'John Doe', 30, 'Manager', 1),
(2, 'Alice Smith', 25, 'Developer', 2);

-- Purchases Table
INSERT INTO [dbo].[Purchases] ([PurchaseID], [SupplierID],[PurchaseDate], [TotalAmount], [EmployeeID])
VALUES
(1, 1,'2023-01-15', 0.00, 1),
(2, 2,'2023-02-20', 0.00, 2);

-- Actualizar el TotalAmount en la tabla Purchases
UPDATE [dbo].[Purchases]
SET [TotalAmount] = (SELECT SUM([Quantity] * [UnitPrice]) FROM [dbo].[OrderProducts] WHERE [OrderProductsID] IN (1, 2, 3))
WHERE [PurchaseID] = 1;

-- Actualizar el TotalAmount en la tabla Purchases
UPDATE [dbo].[Purchases]
SET [TotalAmount] = (SELECT SUM([Quantity] * [UnitPrice]) FROM [dbo].[OrderProducts] WHERE [OrderProductsID] IN (4, 5))
WHERE [PurchaseID] = 2;