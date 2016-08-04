IF NOT EXISTS (
	SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[furniture].[Products]') AND type = 'U'
)
BEGIN
	CREATE TABLE [furniture].[Products]
	(
		[Id] INT NOT NULL IDENTITY PRIMARY KEY, 
		[Name] NVARCHAR(120) NOT NULL, 
		[Description] NVARCHAR(250) NULL, 
		[Weight] DECIMAL(7,3) NOT NULL, 
		[BarcodeNumber] NVARCHAR(13) NOT NULL UNIQUE, 
		[Price] MONEY NOT NULL,
		INDEX IX_Name NONCLUSTERED ([Name]),
		INDEX IX_BarcodeNumber NONCLUSTERED ([BarcodeNumber])
	)
END;

-- Добавяне на нови записи
DECLARE @Name NVARCHAR(120)
DECLARE @Description NVARCHAR(250);
DECLARE @Weight DECIMAL(7,3);
DECLARE @BarcodeNumber NVARCHAR(13); 
DECLARE @Price MONEY;
SET @Name = N'Маса #271 IKEA ООД';
SET @Description = N'Кръгла бяла маса с 4 крака предлаган в магазин IKEA';
SET @Weight = 12.4;
SET @BarcodeNumber =  N'1441170443635';
SET @Price = 21.90;
INSERT INTO furniture.Products
VALUES (@Name, @Description, @Weight, @BarcodeNumber, @Price)

-- Добавяне на index на колона
CREATE NONCLUSTERED INDEX IX_BarcodeNumber ON [furniture].[Products]([BarcodeNumber])

-- Добавяне на unique key на колона
ALTER TABLE furniture.Products
ADD UNIQUE ([BarcodeNumber])

-- Добавяне на primary key на колона
ALTER TABLE [furniture].[Products]
ADD PRIMARY KEY ([Name]);

-- Показване на всички записи
SELECT *
FROM [furniture].[Products]

-- Промяна на стойност на даден запис по зададено Id
DECLARE @ProductId INT;
DECLARE @NewValue money;
DECLARE @ColumnToUpdate NVARCHAR(50);
SET @ProductId = 29;
SET @NewValue = N'88.90';
SET @ColumnToUpdate = N'Price';
UPDATE [furniture].[Products]
SET [Price] = @NewValue
WHERE [Id] = @ProductId;

-- Промяна на колона 
ALTER TABLE [furniture].[Products]
ALTER COLUMN [Name] NVARCHAR(120) NOT NULL;

-- Изтриване запис по Id
DECLARE @ProductId INT;
SET @ProductId = 29;
DELETE FROM [furniture].[Products]
WHERE [Id] = @ProductId;

-- Изтриване на таблицата
DROP TABLE [furniture].[Products];

-- Изтриване на index
DROP INDEX IX_Name;

-- Изтриване на constraint
ALTER TABLE [furniture].[Products]
DROP CONSTRAINT UQ__Products__DF352225CD365481;