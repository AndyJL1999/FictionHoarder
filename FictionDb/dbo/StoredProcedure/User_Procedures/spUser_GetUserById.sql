CREATE PROCEDURE [dbo].[spUser_GetUserById]
	@Id int
AS
begin
	select [User].Id, [User].Username, [User].Email, [User].PasswordHash, [User].PasswordSalt
	from dbo.[User]
	where Id = @Id;
end
