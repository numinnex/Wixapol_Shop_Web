CREATE TABLE [db_product].[BaseSpecification]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [BaseSpecName] NVARCHAR(300) NOT NULL, 
    [BaseSpecValue] NVARCHAR(700) NOT NULL
)
