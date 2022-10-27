CREATE PROCEDURE [db_product].[spSpecification_AdvancedSpecificationGetById]
	@Id int
AS
begin
	set nocount on;
	select * from db_product.AdvancedSpecification where Id = @Id;
end
