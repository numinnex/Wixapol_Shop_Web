CREATE PROCEDURE [db_product].[spShoppingCart_Insert]
@ProductId int,
@Count int,
@UserId nvarchar(450)
AS
begin
	set nocount on;
	
	insert into db_product.ShoppingCart(ProductId, UserId, [Count] )
	values(@ProductId, @UserId , @Count  );
end

