CREATE TABLE [db_product].[BaseSpecValues]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [BaseSpecId] INT NOT NULL, 
    [ProductId] INT NOT NULL, 
    [BaseSpecValues] NVARCHAR(600) NOT NULL,
	CONSTRAINT [FK_BaseSpecValues_ToBaseSpec] FOREIGN KEY (BaseSpecId) REFERENCES db_product.BaseSpecification(Id),
	CONSTRAINT [FK_BaseSpecValues_ToProduct] FOREIGN KEY (ProductId) REFERENCES db_product.Product(Id),

)
