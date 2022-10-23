CREATE TABLE [db_product].[Inventory]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ProductId] INT NOT NULL, 
    [PurchasePrice] MONEY NOT NULL, 
    [Quantity] INT NOT NULL, 
    [PurchaseDate] DATETIME2 NOT NULL,
	CONSTRAINT [FK_Inventory_ToProduct] FOREIGN KEY (ProductId) REFERENCES db_product.Product(Id),
)
