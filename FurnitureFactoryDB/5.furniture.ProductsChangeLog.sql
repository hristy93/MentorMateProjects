IF NOT EXISTS (
	SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[furniture].[ProductsChangeLog]') AND type = 'U'
)
BEGIN
	CREATE TABLE [furniture].[ProductsChangeLog]
	(
		[Id] INT NOT NULL IDENTITY PRIMARY KEY, 
		[Date] SMALLDATETIME NOT NULL, 
		[ProductId] INT NOT NULL,
		[Column] NVARCHAR(50) NOT NULL, 
		[OldValue] NVARCHAR(250) NULL, 
		[NewValue] NVARCHAR(250) NULL,
		[User] VARCHAR(100) NOT NULL
	)
END;

ALTER TABLE [furniture].[ProductsChangeLog]
ADD [User] VARCHAR(100)

ALTER TABLE [furniture].[ProductsChangeLog]
DROP CONSTRAINT UQ__Products__77387D073EDF87DE;

DROP TABLE [furniture].[ProductsChangeLog]


