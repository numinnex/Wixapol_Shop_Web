CREATE PROCEDURE [db_product].[spCategory_GetById]
@Id int
AS

begin
	set nocount on;

	select * from db_product.Category where Category.Id = @Id;

end
