CREATE USER PlaceHolderForSQLUser FOR LOGIN PlaceHolderForSQLUser;
GO
EXEC sp_addrolemember 'db_ddladmin', PlaceHolderForSQLUser;
EXEC sp_addrolemember 'db_securityadmin', PlaceHolderForSQLUser;
EXEC sp_addrolemember 'db_datareader', PlaceHolderForSQLUser;
EXEC sp_addrolemember 'db_datawriter', PlaceHolderForSQLUser;
GO