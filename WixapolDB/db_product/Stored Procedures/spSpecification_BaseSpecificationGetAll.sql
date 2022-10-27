CREATE PROCEDURE [db_product].[spSpecification_BaseSpecificationGetAll]
AS
begin
	set nocount on;
	select * from db_product.BaseSpecification;
end
