CREATE PROCEDURE [dbo].[spStory_Insert]
	@UserId int,
	@Title nvarchar(50),
	@Author nvarchar(50), 
    @Summary nvarchar(1000), 
    @Chapters varchar(3)

AS
begin
	insert into dbo.[Story] (UserId, Title, Author, Summary, Chapters)
	values (@UserId, @Title, @Author, @Summary, @Chapters);
end
