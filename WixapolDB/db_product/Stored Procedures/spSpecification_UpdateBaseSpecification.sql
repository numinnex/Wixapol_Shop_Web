CREATE PROCEDURE [db_product].[spSpecification_UpdateBaseSpecification]
@Id int,
@SpecName nvarchar(300),
@SpecValue nvarchar(700)

AS
begin
	set nocount on;
	update db_product.BaseSpecification
	set SpecName = @SpecName , SpecValue = @SpecValue
	where Id = @Id
end

