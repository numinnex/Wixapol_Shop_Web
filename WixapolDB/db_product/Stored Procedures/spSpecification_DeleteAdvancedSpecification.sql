CREATE PROCEDURE [db_product].[spSpecification_DeleteAdvancedSpecification]
@Id int
AS

begin
	set nocount on;
	delete from db_product.AdvancedSpecification where AdvancedSpecification.Id = @Id;


end
