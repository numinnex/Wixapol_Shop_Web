CREATE PROCEDURE [db_product].[spProduct_Insert]
@Name nvarchar(200),
@CategoryId int,
@ProducentId int,
@AdvancedSpecId int,
@PhysicalSpecId int,
@BaseSpecId int,
@ShortDescription nvarchar(100),
@LongDescription nvarchar(MAX),
@ProductImage nvarchar(300),
@IsDiscounted bit,
@DiscountAmount float,
@DiscountCode nvarchar(50),
@QuantityInStock int,
@TaxRate float,
@RetailPrice money,
@WarrantyLength nvarchar(50)

AS
begin

	set nocount on;

	insert into db_product.Product(
	[Name],
	CategoryId,
	ProducentId,
	AdvancedSpecId,
	PhysicalSpecId,
	BaseSpecId,
	ShortDescription,
	LongDescription,
	ProductImage,
	IsDiscounted,
	DiscountAmount,
	DiscountCode,
	QuantityInStock,
	TaxRate,
	RetailPrice,
	WarrantyLength

	)

	values
	(
	@Name,
	@CategoryId,
	@ProducentId,
	@AdvancedSpecId,
	@PhysicalSpecId,
	@BaseSpecId,
	@ShortDescription,
	@LongDescription,
	@ProductImage,
	@IsDiscounted,
	@DiscountAmount,
	@DiscountCode,
	@QuantityInStock,
	@TaxRate,
	@RetailPrice,
	@WarrantyLength

	);


end

