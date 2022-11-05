CREATE PROCEDURE [db_product].[spShoppingCart_IncrementCount]
	@CartId int
AS
begin
	update db_product.ShoppingCart
	set
	Count = Count + 1
	where Id = @CartId;
end
