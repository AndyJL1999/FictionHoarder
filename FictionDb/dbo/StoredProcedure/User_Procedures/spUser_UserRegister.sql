CREATE PROCEDURE [dbo].[spUser_UserRegister]
	@Username nvarchar(50),
	@PasswordHash varbinary(max),
	@PasswordSalt varbinary(max),
	@Email nvarchar(100)
AS
begin
	insert into dbo.[User] (Username, PasswordHash, PasswordSalt, Email)
	values (@Username, @PasswordHash, @PasswordSalt, @Email)
end
