CREATE PROCEDURE [db_product].[spOrder_Update]
@Id int,
@PhoneNumber nvarchar(350),
@Email nvarchar(450),
@Adress nvarchar(450),
@City nvarchar(450),
@PostalCode nvarchar(150),
@Name nvarchar(450)
AS
begin
	set nocount on;
	update db_product.[Order]
	set PhoneNumber = @PhoneNumber,
	Email = @Email,
	Adress = @Adress,
	City = @City,
	PostalCode = @PostalCode,
	[Name] = @Name

	where Id = @Id;

end
