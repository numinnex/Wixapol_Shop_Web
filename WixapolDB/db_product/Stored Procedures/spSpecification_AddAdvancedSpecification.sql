CREATE PROCEDURE [db_product].[spSpecification_AddAdvancedSpecification]
@SpecName nvarchar(400),
@SpecValue nvarchar(800)

AS
begin
	set nocount on;
	insert into db_product.AdvancedSpecification(AdvancedSpecName , AdvancedSpecValue)
	values(@SpecName , @SpecValue)

	SELECT SCOPE_IDENTITY()

end

