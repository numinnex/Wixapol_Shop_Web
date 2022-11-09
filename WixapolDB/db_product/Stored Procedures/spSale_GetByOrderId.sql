CREATE PROCEDURE [db_product].[spSale_GetByOrderID]
@Id int
AS
begin
	set nocount on;

	select * from db_product.Sale where OrderId = @Id;
end
