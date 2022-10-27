CREATE PROCEDURE [db_product].[spCategory_InsertCategory]
	@Name nvarchar(200)
AS
begin
	set nocount on;
	insert into db_product.Category([Name])
	values (@Name);
end
