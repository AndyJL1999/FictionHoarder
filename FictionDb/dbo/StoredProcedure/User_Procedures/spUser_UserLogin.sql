CREATE PROCEDURE [dbo].[spUser_UserLogin]
	@Email nvarchar(50),
	@PasswordHash varbinary(max),
	@PasswordSalt varbinary(max)
AS
begin
	select [User].[Id], [User].[Username], [User].[PasswordHash], [User].[PasswordSalt], [User].[Email]
	from dbo.[User]
	where Email = @Email and [PasswordHash] = @PasswordHash and [PasswordSalt] = @PasswordSalt
end
