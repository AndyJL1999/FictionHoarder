CREATE PROCEDURE [dbo].[spUser_GetUserByUsername]
	@Username nvarchar(50)
AS
begin
	select [User].Id, [User].Username, [User].Email, [User].PasswordHash, [User].PasswordSalt
	from dbo.[User]
	where Username = @Username;
end
