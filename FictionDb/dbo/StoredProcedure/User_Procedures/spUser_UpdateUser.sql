CREATE PROCEDURE [dbo].[spUser_UpdateUser]
	@Id int,
	@Username nvarchar(50),
	@PasswordHash varbinary(max), 
    @PasswordSalt varbinary(max), 
    @Email NVARCHAR(100)
AS
begin
	update dbo.[User]
	set Username = @Username, PasswordHash = @PasswordHash, PasswordSalt = @PasswordSalt, Email = @Email
	where Id = @Id;
end
