CREATE PROCEDURE [dbo].[spUser_GetUserByNameOrEmail]
	@Email nvarchar(100)
AS
begin
	select [User].Id, [User].Username, [User].Email, [User].PasswordHash, [User].PasswordSalt
	from dbo.[User]
	where Email = @Email;
end
