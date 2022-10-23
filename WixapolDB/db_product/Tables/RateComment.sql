CREATE TABLE [db_product].[RateComment]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [RateId] INT NOT NULL, 
    [UserId] NVARCHAR(450) NOT NULL, 
    [Like] BIT NULL,
    [Dislike] BIT NULL, 
    [Comment] NVARCHAR(300) NULL,
	CONSTRAINT [FK_RateComment_ToUser] FOREIGN KEY (UserId) REFERENCES dbo.AspNetusers(Id),
    CONSTRAINT [FK_RateComment_ToRate] FOREIGN KEY (RateId) REFERENCES db_product.Rate(Id),
)
