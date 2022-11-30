CREATE PROCEDURE [dbo].[spStory_Get]
	@Id int
AS
begin
	select [Story].Id, [Story].Title, [Story].Author, [Story].Summary, [Story].Chapters, [Story].EpubFile
	from dbo.[Story]
	where Id = @Id;
end
