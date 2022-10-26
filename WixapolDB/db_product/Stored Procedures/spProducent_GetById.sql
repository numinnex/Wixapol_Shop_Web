CREATE PROCEDURE [db_product].[spProducent_GetById]
@Id int
AS

begin
	set nocount on;

	select * from db_product.Producent where Producent.Id = @Id;

end
