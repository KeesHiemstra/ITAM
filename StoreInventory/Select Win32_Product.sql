USE ITAM
GO

-- TRUNCATE TABLE Win32_Product

--SELECT *
--FROM Win32_Product
--ORDER BY [Name]


SELECT * 
FROM Win32_Product
WHERE [ComputerName] = 'PC-MARINUS'
	AND [DTDeletion] IS NULL
--	AND [DTCheck] != '2019-04-16 10:49:41.0878466'
