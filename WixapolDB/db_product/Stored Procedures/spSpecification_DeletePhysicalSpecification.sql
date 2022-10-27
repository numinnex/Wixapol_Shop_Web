CREATE PROCEDURE [db_product].[spSpecification_DeletePhysicalSpecification]
@Id int
AS

begin
	set nocount on;
	delete from db_product.PhysicalSpecification where PhysicalSpecification.Id = @Id;


end
