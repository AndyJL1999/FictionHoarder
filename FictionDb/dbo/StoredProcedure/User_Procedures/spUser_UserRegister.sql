CREATE PROCEDURE [dbo].[spUser_UserRegister]
	@Username nvarchar(50),
	@Password nvarchar(50),
	@Email nvarchar(100)
AS
begin
	insert into dbo.[User] (Username, [Password], Email)
	values (@Username, @Password, @Email)
end
