CREATE PROCEDURE [db_product].[spRate_GetByProductId]
@ProductId int
AS
begin

	set nocount on;
	select * from db_product.Rate where ProductId = @ProductId;

end
