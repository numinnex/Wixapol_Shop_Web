CREATE PROCEDURE [db_product].[spOrder_GetById]
@Id int
AS
begin

	set nocount on;
	select * from db_product.[Order] where Id = @Id;
end
