CREATE PROCEDURE [db_product].[spShoppingCart_DecrementCount]
	@CartId int
AS
begin
	update db_product.ShoppingCart
	set
	Count = Count -  1
	where Id = @CartId;
end
