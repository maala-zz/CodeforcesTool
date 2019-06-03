IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Problems] (
    [Id] uniqueidentifier NOT NULL,
    [Difficulty] int NOT NULL,
    [Link] nvarchar(max) NULL,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_Problems] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Tags] (
    [Id] uniqueidentifier NOT NULL,
    [Title] nvarchar(max) NULL,
    CONSTRAINT [PK_Tags] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Users] (
    [Id] uniqueidentifier NOT NULL,
    [Avatar] nvarchar(max) NULL,
    [Contribution] int NOT NULL,
    [Handle] nvarchar(max) NULL,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [ProblemTags] (
    [ProblemId] uniqueidentifier NOT NULL,
    [TagId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_ProblemTags] PRIMARY KEY ([ProblemId], [TagId]),
    CONSTRAINT [FK_ProblemTags_Problems_ProblemId] FOREIGN KEY ([ProblemId]) REFERENCES [Problems] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProblemTags_Tags_TagId] FOREIGN KEY ([TagId]) REFERENCES [Tags] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [UserProblems] (
    [UserId] uniqueidentifier NOT NULL,
    [ProblemId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_UserProblems] PRIMARY KEY ([UserId], [ProblemId]),
    CONSTRAINT [FK_UserProblems_Problems_ProblemId] FOREIGN KEY ([ProblemId]) REFERENCES [Problems] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserProblems_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_ProblemTags_TagId] ON [ProblemTags] ([TagId]);

GO

CREATE INDEX [IX_UserProblems_ProblemId] ON [UserProblems] ([ProblemId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190524134728_init', N'2.0.0-rtm-26452');

GO

