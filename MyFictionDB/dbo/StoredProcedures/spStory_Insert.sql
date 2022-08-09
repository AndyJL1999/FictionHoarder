CREATE PROCEDURE [dbo].[spStory_Insert]
	@Id int,
	@Title nvarchar(50),
	@Author nvarchar(50),
	@Summary nvarchar(200)
AS
begin
	insert into dbo.[Story] (Id, Title, Author, Summary)
	values (@Id, @Title, @Author, @Summary);
end
