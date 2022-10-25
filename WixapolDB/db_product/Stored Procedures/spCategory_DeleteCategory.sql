CREATE PROCEDURE [db_product].[spCategory_DeleteCategory]
@Id int
AS

begin
	set nocount on;
	delete from db_product.Category where Category.Id = @Id;


end
