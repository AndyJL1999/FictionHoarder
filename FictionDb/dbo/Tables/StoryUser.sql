CREATE TABLE [dbo].[StoryUser]
(
	[StoryId] INT NOT NULL, 
    [UserId] INT NOT NULL,
    PRIMARY KEY ([StoryId], [UserId]),
	CONSTRAINT [TK_User_Story] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]),
	CONSTRAINT [TK_Story_User] FOREIGN KEY ([StoryId]) REFERENCES [Story]([Id]) 
)
