CREATE PROCEDURE [dbo].[spStory_Update]
	@Id int,
	@Title nvarchar(50),
	@Author nvarchar(50), 
    @Summary nvarchar(1000), 
    @Chapters varchar(3),
	@EpubFile nvarchar(max)
AS
begin
	update dbo.[Story]
	set Title = @Title, Author = @Author, Summary = @Summary, Chapters = @Chapters, EpubFile = @EpubFile
	where Id = @Id;
end
