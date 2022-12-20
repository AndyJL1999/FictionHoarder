CREATE PROCEDURE [dbo].[spStory_Get]
	@Title nvarchar(50),
	@Author nvarchar(50), 
	@EpubFile nvarchar(max)
AS
begin
	select [Story].Id, [Story].Title, [Story].Author, [Story].Summary, [Story].Chapters, [Story].EpubFile
	from dbo.[Story]
	where Title = @Title and Author = @Author and EpubFile = @EpubFile
end
