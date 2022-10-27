CREATE PROCEDURE [db_product].[spSpecification_PhysicalSpecificationGetAll]
AS
begin
	set nocount on;
	select * from db_product.PhysicalSpecification;
end
