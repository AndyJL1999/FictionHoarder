/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

if not exists (select 1 from dbo.[Story])
begin
    insert into dbo.[Story] (Title, Author, Summary, Chapters)
    values ('White', 'NeonZangetsu', 'A story about life and death.', '4'),
    ('Black', 'NeonZangetsu', 'A story about life and death. Lets add a little more flavor to this summary. Sounds good right?', '7'),
    ('Orange', 'NeonZangetsu', 'Gotta love the color orange. If you don''t like it then you''re dumb.', '2'),
    ('Red', 'NeonZangetsu', 'A story about life and death. Here we go again. I wish things could be simple.', '10'),
    ('Blue', 'NeonZangetsu', 'Blue like the ocean and sky. Nothing like a blue colored thing. Don''t you think the same?', '1'),
    ('Green', 'NeonZangetsu', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut 
    labore et dolore magna aliqua. Ultrices dui sapien eget mi proin. Quam adipiscing vitae proin sagittis nisl rhoncus mattis. 
    Dignissim enim sit amet venenatis urna cursus eget nunc. Id aliquet lectus proin nibh nisl. Sit amet risus nullam eget felis eget 
    nunc lobortis mattis. Sem nulla pharetra diam sit amet nisl suscipit adipiscing. Blandit cursus risus at ultrices mi tempus 
    imperdiet. Mus mauris vitae ultricies leo. Augue interdum velit euismod in pellentesque massa placerat duis. Faucibus turpis in eu 
    mi bibendum.', '34')
end