CREATE PROCEDURE [db_product].[spSale_UpdateSessionInfo]
@Id int,
@SessionId nvarchar(MAX),
@PaymentIntentId nvarchar(MAX)
AS
begin

	set nocount on
	update db_product.Sale
	set SessionId = @SessionId,
	PaymentIntentId = @PaymentIntentId

	where Id = @Id;
end
