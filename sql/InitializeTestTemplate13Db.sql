USE master
GO

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'TestTemplate13Db')
BEGIN
  CREATE DATABASE TestTemplate13Db;
END;
GO

USE TestTemplate13Db;
GO

IF NOT EXISTS (SELECT 1
                 FROM sys.server_principals
                WHERE [name] = N'TestTemplate13Db_Login' 
                  AND [type] IN ('C','E', 'G', 'K', 'S', 'U'))
BEGIN
    CREATE LOGIN TestTemplate13Db_Login
        WITH PASSWORD = '<DB_PASSWORD>';
END;
GO  

IF NOT EXISTS (select * from sys.database_principals where name = 'TestTemplate13Db_User')
BEGIN
    CREATE USER TestTemplate13Db_User FOR LOGIN TestTemplate13Db_Login;
END;
GO  


EXEC sp_addrolemember N'db_datareader', N'TestTemplate13Db_User';
GO

EXEC sp_addrolemember N'db_datawriter', N'TestTemplate13Db_User';
GO

EXEC sp_addrolemember N'db_ddladmin', N'TestTemplate13Db_User';
GO
