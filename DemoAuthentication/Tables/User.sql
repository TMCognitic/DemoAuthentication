CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL IDENTITY, 
    [Nom] NVARCHAR(75) NOT NULL, 
    [Prenom] NVARCHAR(75) NOT NULL, 
    [Email] NVARCHAR(320) NOT NULL, 
    [Passwd] VARBINARY(64) NOT NULL, 
    [IsAdmin] BIT NOT NULL
        CONSTRAINT DF_User_IsAdmin DEFAULT (0), 
    CONSTRAINT [PK_User] PRIMARY KEY ([Id]),
    CONSTRAINT UK_User_Email Unique (Email)
)
