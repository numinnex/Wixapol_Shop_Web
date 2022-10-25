CREATE PROCEDURE [db_product].[spCategory_GetAll]
AS
begin
	set nocount on;

	select * from db_product.Category;
	
end
