CREATE PROCEDURE [db_product].[spProduct_GetByNamePattern]
@NamePattern nvarchar(200)
AS

begin

	set nocount on;
	select p.Id , p.[Name] ,p.AdvancedSpecId , p.BaseSpecId , p.DiscountAmount , p.DiscountCode
	, p.IsDiscounted , p.LongDescription , p.PhysicalSpecId , p.ProductImage , p.QuantityInStock , p.RetailPrice ,
	p.ShortDescription , p.TaxRate , p.WarrantyLength , p.CategoryId ,c.Id, c.[Name] , p.ProducentId , x.Id, x.[Name]
	from db_product.Product as p left join db_product.Category c on p.CategoryId = c.Id
	inner join db_product.Producent as x on p.ProducentId = x.Id where p.[Name] like '%' + @NamePattern + '%';
	end
