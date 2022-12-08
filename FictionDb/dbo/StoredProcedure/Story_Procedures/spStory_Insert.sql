CREATE PROCEDURE [dbo].[spStory_Insert]
	@UserId int,
	@Title nvarchar(50),
	@Author nvarchar(50), 
    @Summary nvarchar(1000), 
    @Chapters varchar(3),
	@EpubFile nvarchar(max)

AS
if not exists (select 1 from [Story] 
				where Title = @Title
				and Author = @Author
				and Summary = @Summary 
				and Chapters = @Chapters)
begin
	insert into dbo.[Story] (Title, Author, Summary, Chapters, EpubFile)
	values (@Title, @Author, @Summary, @Chapters, @EpubFile)

	exec spStoryUser_InsertRelationship @@IDENTITY, @UserId
end

else
begin
	declare @storyId int
	set @storyId = (select Id from [Story]
				where Title = @Title
				and Author = @Author
				and Summary = @Summary 
				and Chapters = @Chapters)

	if not exists (select 1 from [StoryUser]
					where StoryId = @storyId
					and UserId = @UserId)
	begin
		exec spStoryUser_InsertRelationship @storyId, @UserId
	end
end
