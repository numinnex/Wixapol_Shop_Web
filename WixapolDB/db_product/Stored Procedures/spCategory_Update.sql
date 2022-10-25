CREATE PROCEDURE [db_product].[spCategory_Update]
@Id int,
@Name nvarchar(200)
AS

begin
	set nocount on;
	update db_product.Category
	set Name = @Name
	where Id = @Id;
end

