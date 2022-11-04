CREATE PROCEDURE [db_product].[spShoppingCart_UpdateCountByProductId]
@Id int,
@Count int
AS
begin
	set nocount on;

	update db_product.ShoppingCart
	set [Count] = @Count

	where Id = @Id;
end
