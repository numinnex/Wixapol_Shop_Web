CREATE PROCEDURE [db_product].[spShoppingCart_DeleteByUserId]
@UserId nvarchar(450)
AS
begin
	set nocount on;
	delete from db_product.ShoppingCart where UserId = @UserId;


end
