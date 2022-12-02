CREATE PROCEDURE [dbo].[spUserStoryHistory_GetHistory]
	@UserId int
AS
begin
	select [Story].Id, [Story].Title, [Story].Author, [Story].Summary, [Story].Chapters, [Story].EpubFile
	from dbo.[UserStoryHistory]
	inner join dbo.[Story]
	on [UserStoryHistory].ViewedStoryId = [Story].Id
	where UserId = @UserId
	order by [UserStoryHistory].TimeViewed desc;
end