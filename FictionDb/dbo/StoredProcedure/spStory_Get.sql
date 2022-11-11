CREATE PROCEDURE [dbo].[spStory_Get]
	@Id int
AS
begin
	select *
	from dbo.[Story]
	where Id = @Id;
end
