CREATE PROCEDURE [db_product].[spSpecification_PhysicalSpecificationGetById]
	@Id int
AS
begin
	set nocount on;
	select * from db_product.PhysicalSpecification where Id = @Id;
end
