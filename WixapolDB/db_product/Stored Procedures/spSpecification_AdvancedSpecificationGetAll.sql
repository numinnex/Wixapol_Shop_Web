CREATE PROCEDURE [db_product].[spSpecification_AdvancedSpecificationGetAll]
AS
begin
	set nocount on;
	select * from db_product.AdvancedSpecification;
end
