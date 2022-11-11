CREATE PROCEDURE [dbo].[spStory_Update]
	@Id INT,
	@Title NVARCHAR(50),
	@Author NVARCHAR(50), 
    @Summary NVARCHAR(1000), 
    @Chapters VARCHAR(3)
AS
begin
	update dbo.[Story]
	set Title = @Title, Author = @Author, Summary = @Summary, Chapters = @Chapters
	where Id = @Id;
end
