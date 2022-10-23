CREATE TABLE [db_product].[PhysicalSpecification]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PhysicalSpecName] NVARCHAR(300) NOT NULL, 
    [PhysicalSpecValue] NVARCHAR(600) NOT NULL
)
