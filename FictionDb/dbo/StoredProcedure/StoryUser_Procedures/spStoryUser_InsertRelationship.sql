CREATE PROCEDURE [dbo].[spStoryUser_InsertRelationship]
	@StoryId int,
	@UserId int
AS

begin
	insert into dbo.[StoryUser] (StoryId, UserId)
	values (@StoryId, @UserId);
end
