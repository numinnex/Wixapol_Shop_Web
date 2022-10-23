CREATE TABLE [db_product].[Category]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [BaseSpecId] INT NOT NULL, 
    [Name] NVARCHAR(200) NOT NULL,
	CONSTRAINT [FK_Category_ToBaseSpec] FOREIGN KEY (BaseSpecId) REFERENCES db_product.BaseSpecification(Id),
)
