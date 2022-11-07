CREATE PROCEDURE [db_product].[spShoppingCart_UpdatePrices]
@Id int,
@SubTotal money,
@TaxAmount money,
@Total money
AS
begin
	set nocount on;

	update db_product.ShoppingCart
	set Total = @Total,
	SubTotal = @SubTotal,
	TaxAmount = @TaxAmount

	where Id = @Id;
end
