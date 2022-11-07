CREATE PROCEDURE [db_product].[spSale_UpdateStatus]
@Id int,
@OrderStatus nvarchar(250),
@PaymentStatus nvarchar(250)
AS
begin

	set nocount on
	update db_product.Sale
	set OrderStatus = @OrderStatus,
	PaymentStatus = @PaymentStatus

	where Id = @Id;
end
