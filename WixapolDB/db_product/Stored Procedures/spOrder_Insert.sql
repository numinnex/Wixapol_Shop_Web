CREATE PROCEDURE [db_product].[spOrder_Insert]
@PhoneNumber nvarchar(350),
@Email nvarchar(450),
@Adress nvarchar(450),
@City nvarchar(450),
@PostalCode nvarchar(150),
@Name nvarchar(450)
AS
begin
	set nocount on;
	insert into db_product.[Order](PhoneNumber , EmailAdress , Adress , City , PostalCode ,[Name])
	output Inserted.Id
	values (@PhoneNumber , @Email , @Adress , @City , @PostalCode , @Name);

end
