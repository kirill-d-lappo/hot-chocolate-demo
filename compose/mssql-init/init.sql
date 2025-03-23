USE master
go

CREATE LOGIN $(HCDEMO_DB_USER) with password = '$(HCDEMO_DB_PASSWORD)';
go

CREATE USER $(HCDEMO_DB_USER) FOR login $(HCDEMO_DB_USER);
go

exec master..sp_addsrvrolemember @loginame = N'$(HCDEMO_DB_USER)', @rolename = N'sysadmin'
go

CREATE DATABASE [hcdemo];
go

ALTER DATABASE [hcdemo] SET COMPATIBILITY_LEVEL = 140
GO
