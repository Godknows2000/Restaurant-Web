CREATE TABLE [dbo].[Categories] (
    [Id]           INT NOT NULL,
    [Name]         NVARCHAR (MAX) NOT NULL IDENTITY,
    [DisplayOrder] INT            NOT NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED ([Id] ASC)
);

