CREATE PROCEDURE [db_product].[spSale_GetByOrderStatus]
@Status nvarchar(250)
AS
begin
	set nocount on;
	select * from db_product.Sale where LOWER(OrderStatus) = @Status;
end
