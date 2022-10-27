CREATE PROCEDURE [db_product].[spProduct_Update]
@Id int,
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

	update db_product.Product
	set [Name] = @Name,
	CategoryId = @CategoryId,
	ProducentId = @ProducentId,
	AdvancedSpecId = @AdvancedSpecId,
	PhysicalSpecId = @PhysicalSpecId,
	BaseSpecId = @BaseSpecId,
	ShortDescription = @ShortDescription,
	LongDescription = @LongDescription,
	ProductImage = @ProductImage,
	IsDiscounted = @IsDiscounted,
	DiscountAmount = @DiscountAmount,
	DiscountCode = @DiscountCode,
	QuantityInStock = @QuantityInStock,
	TaxRate = @TaxRate,
	RetailPrice = @RetailPrice,
	WarrantyLength = @WarrantyLength

	where Product.Id = @Id;



end

