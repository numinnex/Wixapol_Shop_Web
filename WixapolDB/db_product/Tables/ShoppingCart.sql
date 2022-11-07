CREATE TABLE [db_product].[ShoppingCart]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Count] INT NOT NULL,
    [DiscountAmount] MONEY NULL,
    [SubTotal] MONEY NULL,
    [TaxAmount] MONEY NULL,
    [Total] MONEY NULL,
    [ProductId] INT NULL, 
    [UserId] NVARCHAR(450) NOT NULL
    CONSTRAINT [FK_ShoppingCart_ToUser] FOREIGN KEY (UserId) REFERENCES dbo.AspNetUsers(Id),
    CONSTRAINT [FK_ShoppingCart_ToProduct] FOREIGN KEY (ProductId) REFERENCES db_product.Product(Id),
)
