IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Authors] (
    [Id] int NOT NULL IDENTITY,
    [Country] nvarchar(max) NULL,
    [FirstName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    CONSTRAINT [PK_Authors] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Books] (
    [Id] int NOT NULL IDENTITY,
    [AuthorId] int NOT NULL,
    [Description] nvarchar(max) NULL,
    [ISBN] bigint NOT NULL,
    [PublicationYear] int NOT NULL,
    [Title] nvarchar(max) NULL,
    CONSTRAINT [PK_Books] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Books_Authors_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [Authors] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Books_AuthorId] ON [Books] ([AuthorId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20180328112243_initial', N'2.0.2-rtm-10011');

GO

CREATE TABLE [Genres] (
    [Id] int NOT NULL IDENTITY,
    [Category] nvarchar(max) NULL,
    CONSTRAINT [PK_Genres] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [BookGenres] (
    [BookId] int NOT NULL,
    [GenreId] int NOT NULL,
    CONSTRAINT [PK_BookGenres] PRIMARY KEY ([BookId], [GenreId]),
    CONSTRAINT [FK_BookGenres_Books_BookId] FOREIGN KEY ([BookId]) REFERENCES [Books] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_BookGenres_Genres_GenreId] FOREIGN KEY ([GenreId]) REFERENCES [Genres] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_BookGenres_GenreId] ON [BookGenres] ([GenreId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20180329230120_addedGenre', N'2.0.2-rtm-10011');

GO

CREATE TABLE [Collection] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_Collection] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [BookCollection] (
    [BookId] int NOT NULL,
    [CollectionId] int NOT NULL,
    CONSTRAINT [PK_BookCollection] PRIMARY KEY ([BookId], [CollectionId]),
    CONSTRAINT [FK_BookCollection_Books_BookId] FOREIGN KEY ([BookId]) REFERENCES [Books] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_BookCollection_Collection_CollectionId] FOREIGN KEY ([CollectionId]) REFERENCES [Collection] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_BookCollection_CollectionId] ON [BookCollection] ([CollectionId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20180401194453_addedCollection', N'2.0.2-rtm-10011');

GO

