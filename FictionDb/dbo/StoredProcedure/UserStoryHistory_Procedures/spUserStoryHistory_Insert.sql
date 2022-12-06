CREATE PROCEDURE [dbo].[spUserStoryHistory_Insert]
	@ViewedStoryId int,
	@UserId int,
	@TimeViewed datetime
AS
if exists
(
	select *
	from dbo.[UserStoryHistory] 
	where ViewedStoryId = @ViewedStoryId and UserId = @UserId
) 
begin
	exec spUserStoryHistory_Update @ViewedStoryId, @UserId, @TimeViewed
end

else
begin
	insert into dbo.[UserStoryHistory] (ViewedStoryId, UserId, TimeViewed)
	values (@ViewedStoryId, @UserId, @TimeViewed);
end
