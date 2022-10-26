CREATE PROCEDURE [db_product].[spProducent_GetAll]
AS
begin
	set nocount on;

	select * from db_product.Producent;
	
end
