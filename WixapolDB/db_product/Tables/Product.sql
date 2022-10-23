﻿CREATE TABLE [db_product].[Product]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [Name] NVARCHAR(200) NOT NULL,
    [CategoryId] INT NOT NULL, 
    [ProducentId] INT NOT NULL, 
    [AdvancedSpecId] INT NOT NULL, 
    [PhysicalSpecId] INT NULL, 
    [ShortDescription] NVARCHAR(300) NOT NULL, 
    [LongDescription] NVARCHAR(MAX) NOT NULL, 
    [ProductImage] NVARCHAR(300) NULL,
    [IsDiscounted] BIT NOT NULL,
    [DiscountAmount] FLOAT NULL, 
    [DiscountCode] NVARCHAR(50) NULL, 
    [QuantityInStock] INT NOT NULL,
    [TaxRate] FLOAT NOT NULL,
    [RetailPrice] MONEY NOT NULL, 
    [WarrantyLength] NVARCHAR(50) NOT NULL, 
	CONSTRAINT [FK_Produt_ToCategory] FOREIGN KEY (CategoryId) REFERENCES db_product.Category(Id),
	CONSTRAINT [FK_Product_ToProducent] FOREIGN KEY (ProducentId) REFERENCES db_product.Producent(Id),
	CONSTRAINT [FK_Product_ToAvancedSpec] FOREIGN KEY (AdvancedSpecId) REFERENCES db_product.AdvancedSpecification(Id),
	CONSTRAINT [FK_Product_ToPhysicalSpec] FOREIGN KEY (PhysicalSpecId) REFERENCES db_product.PhysicalSpecification(Id),

)
