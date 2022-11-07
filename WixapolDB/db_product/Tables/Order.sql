﻿CREATE TABLE [db_product].[Order]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PhoneNumber] NVARCHAR(350) NOT NULL, 
    [EmailAdress] NVARCHAR(450) NOT NULL, 
    [Adress] NVARCHAR(450) NOT NULL, 
    [City] NVARCHAR(450) NOT NULL, 
    [PostalCode] NVARCHAR(150) NOT NULL, 
    [Name] NVARCHAR(450) NOT NULL
)
