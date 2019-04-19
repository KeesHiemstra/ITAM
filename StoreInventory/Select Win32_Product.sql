USE ITAM
GO

-- TRUNCATE TABLE Win32_Product

SELECT *
FROM Win32_Product
WHERE [ComputerName] = 'PC06' --'PC-Marinus'
ORDER BY [Name], [ComputerName]


