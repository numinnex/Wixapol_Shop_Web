CREATE PROCEDURE [db_product].[spSaleDetail_Insert]
@SaleId int,
@ProductId int,
@WarrantyExpiration  datetime2(7),
@DiscountAmount float,
@Quantity int,
@SubTotal money,
@Tax money,
@Total money

AS
begin
	set nocount on;

	insert into db_product.SaleDetail(SaleId, ProductId, WarrantyExpiration, DiscountAmount, Quantity , SubTotal,Tax,Total)
	values(@SaleId, @ProductId, @WarrantyExpiration ,@DiscountAmount , @Quantity, @SubTotal , @Tax , @Total );
end
