CREATE PROCEDURE [dbo].[spStory_Update]
	@Id int,
	@Title nvarchar(50),
	@Author nvarchar(50),
	@Summary nvarchar(200)
AS
begin 
	update dbo.[Story]
	set Title = @Title, Author = @Author, Summary = @Summary
	where Id = @Id;
end
