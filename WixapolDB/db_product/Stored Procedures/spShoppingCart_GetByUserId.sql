CREATE PROCEDURE [db_product].[spShoppingCart_GetByUserId]
@UserId nvarchar(450)
AS
begin
	set nocount on;
	select * from db_product.ShoppingCart where ShoppingCart.UserId = @UserId;
end
