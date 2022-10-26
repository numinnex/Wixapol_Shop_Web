CREATE PROCEDURE [db_product].[spProducent_Insert]
	@Name nvarchar(200),
	@ProducentCode nvarchar(300),
	@EAN nvarchar(300)
AS
begin
	set nocount on;
	insert into db_product.Producent([Name],ProducentCode,EAN)
	values (@Name , @ProducentCode , @EAN);
end
