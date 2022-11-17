IF NOT EXISTS 
    (SELECT name  
     FROM master.sys.server_principals
     WHERE name = 'PlaceHolderForSQLUser')
BEGIN
    CREATE LOGIN [PlaceHolderForSQLUser] WITH PASSWORD = N'PlaceHolderForSQLPassword'
END
