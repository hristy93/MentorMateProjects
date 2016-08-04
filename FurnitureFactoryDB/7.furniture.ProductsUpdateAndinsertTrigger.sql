-- Създаване на лог за промяната на продуктите
CREATE TRIGGER LogProductUpdateOrInsert ON [furniture].[Products]
AFTER UPDATE, INSERT
AS
BEGIN
	SET NOCOUNT ON

	IF UPDATE([Name])
		BEGIN
		INSERT INTO [furniture].[ProductsChangeLog]
		SELECT convert(smalldatetime, CURRENT_TIMESTAMP), I.Id AS 'ProductID', 'Name',  D.[Name] AS 'OldName', I.[Name] AS 'NewName', SUSER_SNAME()
		FROM inserted AS I LEFT OUTER JOIN deleted AS D ON I.[Id] = D.[Id] AND I.[Name] <> D.[Name]
		--FROM inserted AS I LEFT OUTER JOIN deleted AS D ON I.[Id] = D.[Id] AND I.[Name] <> D.[Name]
		--				   LEFT OUTER JOIN [furniture].[Products] OP ON OP.[Name] = D.[Name]
		--				   INNER JOIN [furniture].[Products] NP ON NP.[Name] = I.[Name]		
	    END;	
	
	IF UPDATE([Description])
		BEGIN
		INSERT INTO [furniture].[ProductsChangeLog]
		SELECT CONVERT(smalldatetime, CURRENT_TIMESTAMP), I.Id AS 'ProductID', 'Description', D.[Description] AS 'OldDescription', I.[Description] AS 'NewDescription', SUSER_SNAME()
		FROM inserted AS I LEFT OUTER JOIN deleted AS D ON I.[Id] = D.[Id] AND I.[Description] <> D.[Description]	
	    END;	
			
	IF UPDATE([Weight])
		BEGIN
		INSERT INTO [furniture].[ProductsChangeLog]
		SELECT CONVERT(smalldatetime, CURRENT_TIMESTAMP), I.Id AS 'ProductID', 'Weight', CONVERT(NVARCHAR, D.[Weight]) AS 'OldWeight', CONVERT(NVARCHAR, I.[Weight]) AS 'NewWeight', SUSER_SNAME()
		FROM inserted AS I LEFT OUTER JOIN deleted AS D ON I.[Id] = D.[Id] AND I.[Weight] <> D.[Weight]	
	    END;	
		
	IF UPDATE([BarcodeNumber])
		BEGIN
		INSERT INTO [furniture].[ProductsChangeLog]
		SELECT CONVERT(smalldatetime, CURRENT_TIMESTAMP), I.Id AS 'ProductID', 'BarcodeNumber', D.[BarcodeNumber] AS 'OldDescription', I.[BarcodeNumber] AS 'NewDescription', SUSER_SNAME()
		FROM inserted AS I LEFT OUTER JOIN deleted AS D ON I.[Id] = D.[Id] AND I.[BarcodeNumber] <> D.[BarcodeNumber]
	    END;	
		
	IF UPDATE([Price])
		BEGIN
		INSERT INTO [furniture].[ProductsChangeLog]
		SELECT CONVERT(smalldatetime, CURRENT_TIMESTAMP), I.Id AS 'ProductID', 'Price', CONVERT(NVARCHAR, D.[Price]) AS 'OldWeight', CONVERT(NVARCHAR, I.[Price]) AS 'NewWeight', SUSER_SNAME()
		FROM inserted AS I LEFT OUTER JOIN deleted AS D ON I.[Id] = D.[Id] AND I.[Price] <> D.[Price]	
	    END;	   
END;

SELECT * FROM [furniture].[ProductsChangeLog];