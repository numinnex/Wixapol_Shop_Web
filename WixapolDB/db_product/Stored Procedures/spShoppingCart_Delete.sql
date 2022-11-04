CREATE PROCEDURE [db_product].[spShoppingCart_Delete]
	@Id int 
AS
begin
	set nocount on;
	delete from db_product.ShoppingCart where ShoppingCart.Id = @Id;
end
