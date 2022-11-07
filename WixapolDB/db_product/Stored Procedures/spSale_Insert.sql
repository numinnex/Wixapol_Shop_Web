CREATE PROCEDURE [db_product].[spSale_Insert]
@UserId nvarchar(450),
@SaleDate datetime2(7),
@ShippingDate datetime2(7),
@SubTotal money,
@Tax money,
@Total money,
@OrderStatus nvarchar(250),
@PaymentStatus nvarchar(250),
@TrackingNumber nvarchar(450),
@Carrier nvarchar(450),
@PaymentDate datetime2(7),
@SessionId nvarchar(MAX),
@PaymentIntentId nvarchar(MAX),
@OrderId int

AS
begin
	set nocount on
	insert into db_product.Sale(UserId , SaleDate, ShippingDate, SubTotal, Tax ,Total , OrderStatus , PaymentStatus ,TrackingNumber ,Carrier
		,PaymentDate ,SessionId ,PaymentIntentId, OrderId)
	output inserted.Id
	values(@UserId, @SaleDate , @ShippingDate, @SubTotal , @Tax ,@Total , @OrderStatus, @PaymentStatus , @TrackingNumber , @Carrier
		,@PaymentDate , @SessionId , @PaymentIntentId , @OrderId);

end
