CREATE PROCEDURE [db_product].[spSaleDetail_GetBySaleId]
@SaleId int
AS
begin

	set nocount on;

	select * from db_product.SaleDetail where SaleId = @SaleId;
end

