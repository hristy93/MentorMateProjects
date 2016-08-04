-- 1. Търсене на продукт по част от името
IF (SELECT fulltextserviceproperty('isfulltextinstalled')) = 0
	SELECT 'No fulltext search services installed!' AS WarningMessage;
ELSE
	BEGIN
		CREATE UNIQUE INDEX UQ_Name ON furniture.Products(Name);  
		CREATE FULLTEXT CATALOG ft AS DEFAULT;  
		CREATE FULLTEXT INDEX ON furniture.Products(Name)   
		   KEY INDEX UQ_Name   
		   WITH STOPLIST = SYSTEM;  

		DECLARE @PartialName NVARCHAR(120);
		SET @PartialName = 'двойно';
		SELECT P.Name AS ProductName
		FROM furniture.Products AS P
		WHERE CONTAINS(P.Name, @PartialName);
END;

-- 2. По зададен номер на фактура - продуктите, цените им и клиента, който ги е закупил
DECLARE @BillNumber INT;
SET @BillNumber = 6;
SELECT P.Name AS ProductName, P.Price AS ProductPrice, C.CompanyName AS ClientName
FROM furniture.Products AS P
	JOIN furniture.ProductsSoldHistory AS H ON P.Id = H.ProductId
	JOIN furniture.PurchaseInfo AS I ON I.Id = H.PurchaseInfoId
	JOIN furniture.Clients AS C ON C.Id =  I.ClientId
WHERE I.BillNumber = @BillNumber;

-- 3. По име на клиент - всички покупки от последния месец 
DECLARE @ClientName NVARCHAR(50);
SET @ClientName = N'Mondo ООД';
SELECT  C.CompanyName AS ClientName, I.[Date] AS PurchaseDate
FROM furniture.Products AS P
	JOIN furniture.ProductsSoldHistory AS H ON P.Id = H.ProductId
	JOIN furniture.PurchaseInfo AS I ON I.Id = H.PurchaseInfoId
	JOIN furniture.Clients AS C ON C.Id =  I.ClientId
WHERE C.CompanyName = @ClientName AND I.[Date] <= Convert(date, GetDate()) AND I.[Date] >= DateAdd(month, -1, Convert(date, GetDate()));

-- 4. За всички продукти - по колко бройки са продавани през последния месец
SELECT P.Name AS ProductName, H.Quantity AS ProductQuantity
FROM furniture.Products AS P
	JOIN furniture.ProductsSoldHistory AS H ON P.Id = H.ProductId
	JOIN furniture.PurchaseInfo AS I ON I.Id = H.PurchaseInfoId
	WHERE I.[Date] <= Convert(date, GetDate()) AND I.[Date] >= DateAdd(month, -1, Convert(date, GetDate()));