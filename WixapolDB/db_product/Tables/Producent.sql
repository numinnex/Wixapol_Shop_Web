CREATE TABLE [db_product].[Producent]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(200) NOT NULL, 
    [ProducentCode] NVARCHAR(300) NOT NULL, 
    [EAN] NVARCHAR(300) NOT NULL
)
