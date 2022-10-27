CREATE PROCEDURE [db_product].[spProduct_GetAll]
AS

begin

	set nocount on;
	select * from db_product.Product;
end
