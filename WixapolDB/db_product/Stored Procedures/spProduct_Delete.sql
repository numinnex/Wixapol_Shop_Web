CREATE PROCEDURE [db_product].[spProduct_Delete]
@Id int

AS

begin
	set nocount on;
	delete from db_product.Product where Product.Id = @Id;

end
