CREATE TABLE [db_product].[SaleDetail]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [SaleId] INT NOT NULL, 
    [ProductId] INT NOT NULL, 
    [PurchasePrice] MONEY NOT NULL, 
    [WarrantyExpiration] DATETIME2 NOT NULL, 
    [DiscountAmount] FLOAT NULL, 
    [Quantity] INT NOT NULL, 
    [SubTotal] MONEY NOT NULL, 
    [Tax] MONEY NOT NULL, 
    [Total] MONEY NOT NULL,
    CONSTRAINT [FK_SaleDetail_ToSale] FOREIGN KEY (SaleId) REFERENCES db_product.Sale(Id),
    CONSTRAINT [FK_SaleDetail_ToProduct] FOREIGN KEY (ProductId) REFERENCES db_product.Product(Id)
)
