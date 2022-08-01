CREATE PROCEDURE [dbo].[spStory_Insert]
	@Title nvarchar(50),
	@Author nvarchar(50),
	@Summary nvarchar(200)
AS
begin
	insert into dbo.[Story] (Title, Author, Summary)
	values (@Title, @Author, @Summary);
end
