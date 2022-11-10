CREATE PROCEDURE [db_product].[spSale_GetByUserId]
@UserId nvarchar(450)
AS
begin

	set nocount on;
	select * from db_product.Sale where UserId = @UserId;

end
