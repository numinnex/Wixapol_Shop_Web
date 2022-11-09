CREATE PROCEDURE [db_product].[spSale_UpdateSale]
@Id int,
@OrderStatus nvarchar(250),
@PaymentStatus nvarchar(250),
@Carrier nvarchar(450),
@TrackingNumber nvarchar(450),
@ShippingDate datetime2(7)
AS
begin

	set nocount on
	update db_product.Sale
	set OrderStatus = @OrderStatus,
	PaymentStatus = @PaymentStatus,
	Carrier = @Carrier,
	ShippingDate = @ShippingDate,
	TrackingNumber = @TrackingNumber

	where Id = @Id;
end

