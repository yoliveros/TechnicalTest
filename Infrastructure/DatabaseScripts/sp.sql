USE InventoryDB;
GO

CREATE PROCEDURE dbo.spGetInventoryValueByCategory
AS
BEGIN
	SELECT Category, SUM(Quantity * UnitPrice) AS TotalValue
	FROM Products
	GROUP BY Category;
END
GO
