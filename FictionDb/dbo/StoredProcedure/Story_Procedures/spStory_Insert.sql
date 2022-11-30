CREATE PROCEDURE [dbo].[spStory_Insert]
	@Title nvarchar(50),
	@Author nvarchar(50), 
    @Summary nvarchar(1000), 
    @Chapters varchar(3),
	@EpubFile nvarchar(max)

AS
begin
if not exists (select 1 from [Story] 
				where Title = @Title 
				and Author = @Author 
				and Summary = @Summary 
				and Chapters = @Chapters)
	begin
		insert into dbo.[Story] (Title, Author, Summary, Chapters, EpubFile)
		values (@Title, @Author, @Summary, @Chapters,@EpubFile)
	end
end
