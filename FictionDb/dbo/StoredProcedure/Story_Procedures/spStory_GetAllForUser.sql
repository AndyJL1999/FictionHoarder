CREATE PROCEDURE [dbo].[spStory_GetAllForUser]
	@UserId int

AS
begin
	select [Story].Id, [Story].Title, [Story].Author, [Story].Summary, [Story].Chapters
	from dbo.[StoryUser]
	inner join dbo.[Story]
	on [StoryUser].StoryId = [Story].Id
	where UserId = @UserId
	order by [Story].Title;
end
