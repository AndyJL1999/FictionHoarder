CREATE PROCEDURE [dbo].[spUser_GetUserByNameOrEmail]
	@NameOrEmail nvarchar(100)
AS
begin
	select [User].Id, [User].Username, [User].Email, [User].PasswordHash, [User].PasswordSalt
	from dbo.[User]
	where Username = @NameOrEmail or Email = @NameOrEmail;
end
