CREATE PROCEDURE [dbo].[spStory_Delete]
	@Id int
AS
begin
	delete
	from dbo.[Story]
	where Id = @Id;
end
