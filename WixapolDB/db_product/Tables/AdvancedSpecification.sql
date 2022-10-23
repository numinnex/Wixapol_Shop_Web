CREATE TABLE [db_product].[AdvancedSpecification]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [AdvancedSpecName] NVARCHAR(400) NOT NULL,
    [AdvancedSpecValue] NVARCHAR(800) NOT NULL
)
