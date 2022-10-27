CREATE PROCEDURE [db_product].[spProducent_DeleteProducent]
@Id int
AS

begin
	set nocount on;
	delete from db_product.Producent where Producent.Id = @Id;

end

