CREATE PROCEDURE [db_product].[spProduct_DecreaseQuantity]
@Id int,
@Quantity int
AS
begin
	set nocount on;
	update db_product.Product
	set QuantityInStock = QuantityInStock - @Quantity
	where Id = @Id;


end