CREATE TABLE [db_product].[Sale]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] NVARCHAR(450) NOT NULL, 
    [SaleDate] DATETIME2 NOT NULL,
    [ShippingDate] DATETIME2 NULL, 
    [Total] MONEY NOT NULL, 
    [OrderStatus] NVARCHAR(250) NULL, 
    [PaymentStatus] NVARCHAR(250) NULL, 
    [SessionId] NVARCHAR(MAX) NULL, 
    [PaymentIntentId] NVARCHAR(MAX) NULL, 
    [OrderId] INT NULL, 
    CONSTRAINT [FK_Sale_ToUser] FOREIGN KEY (UserId) REFERENCES dbo.AspNetusers(Id),

)
