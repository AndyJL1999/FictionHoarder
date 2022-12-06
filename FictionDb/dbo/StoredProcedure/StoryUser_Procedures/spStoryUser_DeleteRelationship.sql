CREATE PROCEDURE [dbo].[spStoryUser_DeleteRelationship]
	@StoryId int,
	@UserId int
AS
begin
	delete
	from dbo.[StoryUser]
	where StoryId = @StoryId and UserId = @UserId
end
