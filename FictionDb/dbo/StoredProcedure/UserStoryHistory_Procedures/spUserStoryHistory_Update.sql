CREATE PROCEDURE [dbo].[spUserStoryHistory_Update]
	@ViewedStoryId int,
	@UserId int,
	@TimeViewed datetime
AS
begin
	update dbo.[UserStoryHistory]
	set ViewedStoryId = @ViewedStoryId, UserId = @UserId, TimeViewed = @TimeViewed
	where ViewedStoryId = @ViewedStoryId and UserId = @UserId
end
