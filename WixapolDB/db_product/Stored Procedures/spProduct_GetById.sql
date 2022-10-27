CREATE PROCEDURE [db_product].[spProduct_GetById]
@Id int

AS
begin
	set nocount on;
	select * from db_product.Product where Product.Id = @Id;

end

