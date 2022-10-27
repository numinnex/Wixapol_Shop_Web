CREATE PROCEDURE [db_product].[spSpecification_UpdatePhysicalSpecification]
@Id int,
@SpecName nvarchar(300),
@SpecValue nvarchar(600)

AS
begin
	set nocount on;
	update db_product.PhysicalSpecification	
	set PhysicalSpecName = @SpecName , PhysicalSpecValue = @SpecValue
	where Id = @Id
end

