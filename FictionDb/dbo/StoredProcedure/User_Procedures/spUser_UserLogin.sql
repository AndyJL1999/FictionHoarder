CREATE PROCEDURE [dbo].[spUser_UserLogin]
	@Email nvarchar(50),
	@Password nvarchar(100)
AS
begin
	select [User].[Id], [User].[Username], [User].[Password], [User].[Email]
	from dbo.[User]
	where Email = @Email and [Password] = @Password;
end
