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
	[TrackingNumber] NVARCHAR(450) NULL, 
    [Carrier] NVARCHAR(450) NULL, 
    [PaymentDate] DATETIME2 NULL, 
    [SessionId] NVARCHAR(MAX) NULL, 
    [PaymentIntentId] NVARCHAR(MAX) NULL, 
    [OrderId] INT NOT NULL, 
    CONSTRAINT [FK_Sale_ToUser] FOREIGN KEY (UserId) REFERENCES dbo.AspNetusers(Id),
    CONSTRAINT [FK_Sale_ToOrder] FOREIGN KEY (OrderId) REFERENCES db_product.[Order](Id) 

)
