CREATE PROCEDURE [db_product].[spSale_GetAll]
AS
begin
	set nocount on;
	select * from db_product.Sale;
end
