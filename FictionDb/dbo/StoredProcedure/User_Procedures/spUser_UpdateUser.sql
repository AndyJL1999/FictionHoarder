CREATE PROCEDURE [dbo].[spUser_UpdateUser]
	@Id int,
	@Username nvarchar(50),
	@PasswordHash varbinary(max), 
    @PasswordSalt varbinary(max), 
    @Email NVARCHAR(100)
AS
if not exists
(
	select [User].Username, [User].Email
	from dbo.[User]
	where Username = @Username and Email = @Email
)
begin
	update dbo.[User]
	set Username = @Username, PasswordHash = @PasswordHash, PasswordSalt = @PasswordSalt, Email = @Email
	where Id = @Id;
end
