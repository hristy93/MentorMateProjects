IF NOT EXISTS (
	SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[furniture].[Clients]') AND type = 'U'
)
BEGIN
	CREATE TABLE [furniture].[Clients]
	(
		[Id] INT NOT NULL IDENTITY PRIMARY KEY, 
		[CompanyName] NVARCHAR(50) NOT NULL, 
		[Address] NVARCHAR(120) NULL, 
		[Bulstat] BIGINT NOT NULL UNIQUE, 
		[IsRegisteredByVAT] BIT NOT NULL DEFAULT 0, 
		[PersonInCharge] NVARCHAR(50) NOT NULL,
		INDEX IX_Address NONCLUSTERED ([Address]),
		INDEX IX_Bulstat NONCLUSTERED ([Bulstat])
	);
END;

-- Добавяне на нови записи
CREATE PROCEDURE [furniture].[AddNewClient] (
	@CompanyName NVARCHAR(50),
	@Address NVARCHAR(120),
	@Bulstat BIGINT,
	@IsRegisteredByVAT BIT,
	@PersonInCharge NVARCHAR(50)
)
AS 
BEGIN
--DECLARE	@CompanyName NVARCHAR(50);
--DECLARE	@Address NVARCHAR(120);
--DECLARE	@Bulstat BIGINT; 
--DECLARE	@IsRegisteredByVAT BIT; 
--DECLARE	@PersonInCharge NVARCHAR(50);
--SET @CompanyName = N'Виденов ООД';
--SET @Address = N'гр. Пловдив, бул. България 25';
--SET @Bulstat =  151435289;
--SET @IsRegisteredByVAT = 0;
--SET @PersonInCharge = N'Симеон Ганчев';
	INSERT INTO [furniture].[Clients]
	VALUES (@CompanyName, @Address, @Bulstat, @IsRegisteredByVAT, @PersonInCharge)
END;

-- Добавяне на index на колона
CREATE NONCLUSTERED INDEX IX_Address ON [furniture].[Clients]([Address])

-- Добавяне на unique key на колона
ALTER TABLE [furniture].[Clients]
ADD UNIQUE ([CompanyName])

-- Добавяне на primary key на колона
ALTER TABLE [furniture].[Clients]
ADD PRIMARY KEY ([CompanyName]);

-- Показване на всички записи
SELECT *
FROM [furniture].[Clients]

-- Промяна на стойност на даден запис по зададено Id
CREATE PROCEDURE [furniture].[UpdateClientById] (
	@ProductId INT,
	@NewValue NVARCHAR(50),
	@ColumnToUpdate NVARCHAR(50)
)
AS
BEGIN
--DECLARE @ProductId INT;
--DECLARE @NewValue NVARCHAR(50);
--DECLARE @ColumnToUpdate NVARCHAR(50);
--SET @ClientId = 10;
--SET @NewValue = N'Виденов ООД';
--SET @ColumnToUpdate = N'CompanyName';
	UPDATE [furniture].[Clients]
	SET @ColumnToUpdate = @NewValue
	WHERE Id = @ClientId
END;

-- Промяна на колона 
ALTER TABLE [furniture].[Clients]
ALTER COLUMN [CompanyName] NVARCHAR(50) NOT NULL;

-- Изтриване запис по Id
DECLARE @ClientId INT;
SET @ClientId = 2;
DELETE FROM [furniture].[Clients]
WHERE [Id] = @ClientId;

-- Изтриване на таблицата
DROP TABLE [furniture].[Clients];

-- Изтриване на index
DROP INDEX IX_Address;

-- Изтриване на constraint
ALTER TABLE [furniture].[Clients]
DROP CONSTRAINT PK__Clients__3214EC07CB7320B4;