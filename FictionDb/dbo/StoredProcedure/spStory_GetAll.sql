CREATE PROCEDURE [dbo].[spStory_GetAll]
	@UserId int

AS
begin
	select *
	from dbo.[Story]
	where UserId = @UserId;
end
