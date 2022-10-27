CREATE PROCEDURE [db_product].[spSpecification_AddBasicSpecification]
@SpecName nvarchar(300),
@SpecValue nvarchar(700)

AS
begin
	set nocount on;
	insert into db_product.BaseSpecification(BaseSpecName , BaseSpecValue)
	values(@SpecName , @SpecValue)

	SELECT SCOPE_IDENTITY()

end

