CREATE TABLE [dbo].[UserStoryHistory]
(
	[ViewedStoryId] INT NOT NULL, 
    [UserId] INT NOT NULL,
	[TimeViewed] DATETIME NOT NULL, 
    PRIMARY KEY ([ViewedStoryId], [UserId]),
	CONSTRAINT [TK_User_StoryHistory] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]),
	CONSTRAINT [TK_Story_UserHistory] FOREIGN KEY ([ViewedStoryId]) REFERENCES [Story]([Id])
)

