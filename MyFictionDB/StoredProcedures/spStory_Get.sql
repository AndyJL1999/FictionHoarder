CREATE PROCEDURE [dbo].[spStory_Get]
	@Id int
AS
begin
	select Id, Title, Author, Summary
	from dbo.[Story]
	where Id = @Id;
end