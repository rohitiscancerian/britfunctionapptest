CREATE TABLE [dbo].[Item]
(
	[Id] INT NOT NULL  IDENTITY (1,1),
	[ProductId] int NOT NULL,
	[Quantity] int NOT NULL,
	[CreatedBy]  NVARCHAR(100) NOT NULL,
	[CreatedOn]  DATETIME NOT NULL,
	[ModifiedBy]  NVARCHAR(100) NULL,
	[ModifiedOn]  DATETIME NULL, 
    CONSTRAINT [FK_Item_ToTable] FOREIGN KEY ([ProductId]) REFERENCES [Product]([Id]), 
    PRIMARY KEY ([ProductId])

)
