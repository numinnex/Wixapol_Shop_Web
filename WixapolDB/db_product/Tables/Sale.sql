CREATE TABLE [db_product].[Sale]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] NVARCHAR(450) NOT NULL, 
    [SaleDate] DATETIME2 NOT NULL,
    CONSTRAINT [FK_Sale_ToUser] FOREIGN KEY (UserId) REFERENCES dbo.AspNetusers(Id),

)
