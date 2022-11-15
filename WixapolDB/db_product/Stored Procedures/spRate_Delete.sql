CREATE PROCEDURE [db_product].[spRate_Delete]
@Id int

AS
begin
	set nocount on;
	delete from db_product.Rate where Id = @Id;
end
