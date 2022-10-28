CREATE TABLE [db_product].[AdvancedSpecification]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [SpecName] NVARCHAR(400) NOT NULL,
    [SpecValue] NVARCHAR(800) NOT NULL
)
