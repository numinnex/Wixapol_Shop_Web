CREATE PROCEDURE [db_product].[spRate_Insert]
@ProductId int,
@UserId nvarchar(450),
@RateValue int,
@Likes int,
@Dislikes int,
@Text nvarchar(500),
@RateDate datetime2(7)

AS
begin
	set nocount on;
	insert into db_product.Rate(ProductId, UserId,RateValue,Likes,DisLikes, [Text], RateDate)
	values(
	@ProductId,
	@UserId,
	@RateValue,
	@Likes,
	@Dislikes,
	@Text,
	@RateDate
	);


end

