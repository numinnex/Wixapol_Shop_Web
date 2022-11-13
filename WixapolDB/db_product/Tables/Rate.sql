CREATE TABLE [db_product].[Rate]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ProductId] INT NOT NULL, 
    [UserId] NVARCHAR(450) NOT NULL, 
    [RateValue] INT NOT NULL,
    [Likes] INT NOT NULL DEFAULT 0,
    [DisLikes] INT NOT NULL DEFAULT 0,
    [Text] NVARCHAR(500) NULL,
	[RateDate] DATETIME2 NOT NULL DEFAULT getutcdate(), 
    CONSTRAINT [FK_Rate_ToUser] FOREIGN KEY (UserId) REFERENCES dbo.AspNetusers(Id),
	CONSTRAINT [FK_Rate_ToProduct] FOREIGN KEY (ProductId) REFERENCES db_product.Product(Id),
)
