-- Създаване на лог за промяната на продуктите
CREATE TRIGGER LogProductDelete ON [furniture].[Products]
AFTER DELETE
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [furniture].[ProductsChangeLog]
	SELECT CONVERT(smalldatetime, CURRENT_TIMESTAMP), D.Id AS 'ProductID', 'Name', D.[Name] AS 'OldValue', NULL AS 'NewValue', SUSER_SNAME()
	FROM deleted AS D

	INSERT INTO [furniture].[ProductsChangeLog]
	SELECT CONVERT(smalldatetime, CURRENT_TIMESTAMP), D.Id AS 'ProductID', 'Description', D.[Description] AS 'OldValue', NULL AS 'NewValue', SUSER_SNAME()
	FROM deleted AS D --[Products] AS P RIGHT OUTER JOIN deleted AS D ON D.[Id] = P.[Id] AND P.[Description] <> D.[Description]

	INSERT INTO [furniture].[ProductsChangeLog]
	SELECT CONVERT(smalldatetime, CURRENT_TIMESTAMP), D.Id AS 'ProductID', 'Weight', D.[Weight] AS 'OldValue', NULL AS 'NewValue', SUSER_SNAME()
	FROM deleted AS D --[Products] AS P RIGHT OUTER JOIN deleted AS D ON D.[Id] = P.[Id] AND P.[Weight] <> D.[Weight]

	INSERT INTO [furniture].[ProductsChangeLog]
	SELECT CONVERT(smalldatetime, CURRENT_TIMESTAMP), D.Id AS 'ProductID', 'BarcodeNumber', D.[BarcodeNumber] AS 'OldValue', NULL AS 'NewValue', SUSER_SNAME()
	FROM deleted AS D --[Products] AS P RIGHT OUTER JOIN deleted AS D ON D.[Id] = P.[Id] AND P.[Weight] <> D.[Weight]

	INSERT INTO [furniture].[ProductsChangeLog]
	SELECT CONVERT(smalldatetime, CURRENT_TIMESTAMP), D.Id AS 'ProductID', 'Price', D.[Price] AS 'OldValue', NULL AS 'NewValue', SUSER_SNAME()
	FROM deleted AS D --[Products] AS P RIGHT OUTER JOIN deleted AS D ON D.[Id] = P.[Id] AND P.[Price] <> D.[Price]		  
END;

SELECT * FROM [furniture].[ProductsChangeLog];