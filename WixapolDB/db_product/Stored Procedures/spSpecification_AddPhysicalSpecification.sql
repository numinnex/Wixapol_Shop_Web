CREATE PROCEDURE [db_product].[spSpecification_AddPhysicalSpecification]
@SpecName nvarchar(300),
@SpecValue nvarchar(600)

AS
begin
	set nocount on;
	insert into db_product.PhysicalSpecification(SpecName , SpecValue)
	output inserted.Id
	values(@SpecName , @SpecValue)


end

