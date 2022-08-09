CREATE PROCEDURE [dbo].[spStory_GetAll]
AS
begin
	select Id, Title, Author, Summary
	from dbo.[Story];
end
