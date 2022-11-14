CREATE PROCEDURE [dbo].[spStory_Insert]
	@UserId int,
	@Title NVARCHAR(50),
	@Author NVARCHAR(50), 
    @Summary NVARCHAR(1000), 
    @Chapters VARCHAR(3)

AS
begin
	insert into dbo.[Story] (UserId, Title, Author, Summary, Chapters)
	values (@UserId, @Title, @Author, @Summary, @Chapters);
end
