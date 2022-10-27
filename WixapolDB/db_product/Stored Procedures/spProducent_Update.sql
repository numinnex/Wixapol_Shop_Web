CREATE PROCEDURE [db_product].[spProducent_Update]
@Id int,
@Name nvarchar(200),
@ProducentCode nvarchar(300),
@EAN nvarchar(300)
AS

begin
	set nocount on;
	update db_product.Producent
	set [Name] = @Name, ProducentCode = @ProducentCode , EAN = @EAN
	where Id = @Id;
end
