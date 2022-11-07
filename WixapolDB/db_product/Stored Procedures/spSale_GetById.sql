CREATE PROCEDURE [db_product].[spSale_GetById]
@Id int
AS
begin
	set nocount on;

	select * from db_product.Sale where Id = @Id;
end
