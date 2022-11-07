CREATE TABLE [db_product].[Sale]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] NVARCHAR(450) NOT NULL, 
    [SaleDate] DATETIME2 NOT NULL,
    [ShippingDate] DATETIME2 NULL, 
    [SubTotal] MONEY NOT NULL, 
    [Tax] MONEY NOT NULL, 
    [Total] MONEY NOT NULL, 
    [OrderStatus] NVARCHAR(250) NULL, 
    [PaymentStatus] NVARCHAR(250) NULL, 
    [SessionId] NVARCHAR(MAX) NOT NULL, 
    [PaymentIntentId] NVARCHAR(MAX) NOT NULL, 
    [OrderId] INT NOT NULL, 
    CONSTRAINT [FK_Sale_ToUser] FOREIGN KEY (UserId) REFERENCES dbo.AspNetusers(Id),

)
