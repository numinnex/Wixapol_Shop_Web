CREATE PROCEDURE [db_product].[spSpecification_BaseSpecificationGetById]
	@Id int
AS
begin
	set nocount on;
	select * from db_product.BaseSpecification where Id = @Id;
end
