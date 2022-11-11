CREATE PROCEDURE [dbo].[spStory_Insert]
	@Title NVARCHAR(50),
	@Author NVARCHAR(50), 
    @Summary NVARCHAR(1000), 
    @Chapters VARCHAR(3)

AS
begin
	insert into dbo.[Story] (Title, Author, Summary, Chapters)
	values (@Title, @Author, @Summary, @Chapters);
end
