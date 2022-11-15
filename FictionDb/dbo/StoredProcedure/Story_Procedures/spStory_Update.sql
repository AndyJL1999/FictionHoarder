CREATE PROCEDURE [dbo].[spStory_Update]
	@Id int,
	@Title nvarchar(50),
	@Author nvarchar(50), 
    @Summary nvarchar(1000), 
    @Chapters varchar(3)
AS
begin
	update dbo.[Story]
	set Title = @Title, Author = @Author, Summary = @Summary, Chapters = @Chapters
	where Id = @Id;
end
