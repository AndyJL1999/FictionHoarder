CREATE PROCEDURE [dbo].[spUserStoryHistory_Update]
	@Id int,
	@ViewedStoryId int,
	@UserId int,
	@TimeViewed datetime
AS
begin
	update dbo.[UserStoryHistory]
	set ViewedStoryId = @ViewedStoryId, UserId = @UserId, TimeViewed = @TimeViewed
	where Id = @Id
end
