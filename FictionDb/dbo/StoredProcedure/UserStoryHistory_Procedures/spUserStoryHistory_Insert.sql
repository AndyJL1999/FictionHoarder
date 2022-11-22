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
	declare @RecordId int
	set @RecordId = (select Id from dbo.[UserStoryHistory] where ViewedStoryId = @ViewedStoryId and UserId = @UserId)
	exec spUserStoryHistory_Update @RecordId, @ViewedStoryId, @UserId, @TimeViewed
end

else
begin
	insert into dbo.[UserStoryHistory] (ViewedStoryId, UserId, TimeViewed)
	values (@ViewedStoryId, @UserId, @TimeViewed);
end
