CREATE PROCEDURE [db_product].[spSpecification_DeleteBaseSpecification]
@Id int
AS

begin
	set nocount on;
	delete from db_product.BaseSpecification where BaseSpecification.Id = @Id;


end
