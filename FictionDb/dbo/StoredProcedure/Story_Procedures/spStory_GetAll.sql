CREATE PROCEDURE [dbo].[spStory_GetAll]
	@UserId int

AS
begin
	select *
	from dbo.[StoryUser]
	inner join dbo.[Story]
	on [StoryUser].StoryId = [Story].Id
	where UserId = @UserId
	order by [Story].Title;
end
