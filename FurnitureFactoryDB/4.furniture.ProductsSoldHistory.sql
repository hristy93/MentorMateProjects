IF NOT EXISTS (
	SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[furniture].[ProductsSoldHistory]') AND type = 'U'
)
BEGIN
	CREATE TABLE [furniture].[ProductsSoldHistory]
	(
		[Id] INT NOT NULL IDENTITY PRIMARY KEY, 
		[PurchaseInfoId] INT NOT NULL FOREIGN KEY REFERENCES furniture.PurchaseInfo(Id), 
		[ProductId] INT NOT NULL FOREIGN KEY REFERENCES furniture.Products(Id), 
		[Quantity] INT NOT NULL
	);
END;

-- Добавяне на нови записи
CREATE PROCEDURE [furniture].[AddNewProductsSoldHistoryEntry] (
	@PurchaseInfoId INT,
	@ProductId INT,
	@Quantity INT
)
AS
BEGIN
--DECLARE @PurchaseInfoId INT;
--DECLARE @ProductId INT; 
--DECLARE @Quantity INT;
--SET @PurchaseInfoId = 12;
--SET @ProductId = 6;
--SET @Quantity = 15;
	INSERT INTO [furniture].[ProductsSoldHistory]
	VALUES (@PurchaseInfoId, @ProductId, @Quantity)
END;

-- Добавяне на index на колона
CREATE NONCLUSTERED INDEX IX_Quantity ON [furniture].[ProductsSoldHistory]([Quantity])

-- Добавяне на unique key на колона
ALTER TABLE [furniture].[ProductsSoldHistory]
ADD UNIQUE ([Quantity])

-- Добавяне на primary key на колона
ALTER TABLE [furniture].[ProductsSoldHistory]
ADD PRIMARY KEY ([Quantity]);

-- Показване на всички записи
SELECT *
FROM [furniture].[ProductsSoldHistory]

-- Промяна на стойност на даден запис по зададено Id
CREATE PROCEDURE [furniture].[UpdateProductsSoldHistoryEntry] (
	@ProductsSoldHistoryId INT,
	@NewValue INT,
	@ColumnToUpdate NVARCHAR(50)
)
AS
BEGIN
--DECLARE @ProductsSoldHistoryId INT;
--DECLARE @NewValue INT;
--DECLARE @ColumnToUpdate NVARCHAR(50);
--SET @ProductsSoldHistoryId = 10;
--SET @NewValue = 7;
--SET @ColumnToUpdate = N'ProductId';
	UPDATE [furniture].[ProductsSoldHistory]
	SET @ColumnToUpdate = @NewValue
	WHERE [Id] = @ProductsSoldHistoryId
END;

-- Промяна на колона 
ALTER TABLE [furniture].[ProductsSoldHistory]
ALTER COLUMN [Name] NVARCHAR(120) NOT NULL;

-- Изтриване запис по Id
DECLARE @ProductId INT;
SET @ProductId = 2;
DELETE FROM [furniture].[ProductsSoldHistory]
WHERE [Id] = @ProductId;

-- Изтриване на таблицата
DROP TABLE [furniture].[ProductsSoldHistory];

-- Изтриване на index
DROP INDEX IX_Quantity;

-- Изтриване на constraint
ALTER TABLE [furniture].[ProductsSoldHistory]
DROP CONSTRAINT PK__Products__3214EC07459A35A6;
