CREATE PROCEDURE [dbo].[spStory_GetAll]
AS
begin
	select *
	from dbo.[Story];
end
