CREATE PROCEDURE [dbo].[spUserStoryHistory_Delete]
	@StoryId int,
	@UserId int
AS
begin
	delete
	from dbo.[UserStoryHistory]
	where ViewedStoryId = @StoryId and UserId = @UserId
end
