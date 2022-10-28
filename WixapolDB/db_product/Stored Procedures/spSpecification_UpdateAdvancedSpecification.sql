CREATE PROCEDURE [db_product].[spSpecification_UpdateAdvancedSpecification]
@Id int,
@SpecName nvarchar(400),
@SpecValue nvarchar(800)

AS
begin
	set nocount on;
	update db_product.AdvancedSpecification	
	set SpecName = @SpecName , SpecValue = @SpecValue
	where Id = @Id
end

