IF NOT EXISTS (
	SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[furniture].[PurchaseInfo]') AND type = 'U'
)
BEGIN
	CREATE TABLE [furniture].[PurchaseInfo]
	(
		[Id] INT NOT NULL IDENTITY PRIMARY KEY, 
		[Date] DATE NOT NULL, 
		[ClientId] INT NOT NULL FOREIGN KEY REFERENCES furniture.Clients(Id), 
		[BillNumber] INT NOT NULL UNIQUE,
		INDEX IX_Date NONCLUSTERED ([Date])
	);
END;

-- Добавяне на нови записи
DECLARE @Date DATE;
DECLARE @ClientId INT;
DECLARE @BillNumber INT
SET @Date = N'2016-05-28';	
SET @ClientId = 2;
SET @BillNumber = 6;
INSERT INTO [furniture].[PurchaseInfo]
VALUES (@Date, @ClientId, @BillNumber);

-- Добавяне на index на колона
CREATE NONCLUSTERED INDEX IX_Date ON [furniture].[PurchaseInfo]([Date])

-- Добавяне на unique key на колона
ALTER TABLE [furniture].[PurchaseInfo]
ADD UNIQUE ([Date])

-- Добавяне на primary key на колона
ALTER TABLE [furniture].[PurchaseInfo]
ADD PRIMARY KEY ([Date]);

-- Показване на всички записи
SELECT *
FROM [furniture].[PurchaseInfo]

-- Промяна на стойност на даден запис по зададено Id
DECLARE @ProductId INT;
DECLARE @NewValue NVARCHAR(120);
DECLARE @ColumnToUpdate NVARCHAR(50);
SET @PurchaseInfoId = 1;
SET @NewValue = N'2016-07-21';
SET @ColumnToUpdate = N'[Date]';
UPDATE [furniture].[PurchaseInfo]
SET @ColumnToUpdate = @NewValue
WHERE [Id] = @PurchaseInfoId;

-- Промяна на колона 
ALTER TABLE [furniture].[PurchaseInfo]
ALTER COLUMN [Date] DATE NOT NULL;

-- Изтриване запис по Id
DECLARE @PurchaseInfoId INT;
SET @PurchaseInfoId = 2;
DELETE FROM [furniture].[PurchaseInfo]
WHERE [Id] = @PurchaseInfoId;

-- Изтриване на таблицата
DROP TABLE [furniture].[PurchaseInfo];

-- Изтриване на index
DROP INDEX IX_Date;

-- Изтриване на constraint
ALTER TABLE [furniture].[PurchaseInfo]
DROP CONSTRAINT UQ__Purchase__C4BBE0C64507B1FD;