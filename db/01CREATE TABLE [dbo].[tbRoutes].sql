GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;
SET NUMERIC_ROUNDABORT OFF;
GO

declare @DatabaseName      as varchar(20) = 'BancoMaster'
declare @DefaultFilePrefix as varchar(20) = 'BancoMaster'
declare @DefaultDataPath   as varchar(100)= '/var/opt/mssql/data/'
declare @DefaultLogPath    as varchar(100)= '/var/opt/mssql/data/'
GO

USE BancoMaster;
GO

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
GO
BEGIN TRANSACTION
GO
PRINT N'Creating Table [dbo].[tbRoutes]...';

GO
CREATE TABLE [dbo].[tbRoutes] (
    [rtsGuid]       UNIQUEIDENTIFIER NOT NULL,
    [rtsOrigin]     NVARCHAR (3)     NOT NULL,
    [rtsDestintion] NVARCHAR (3)     NOT NULL,
    [rtsPrice]      DECIMAL (5, 2)   NOT NULL,
    CONSTRAINT [PK_tbRoutes] PRIMARY KEY CLUSTERED ([rtsGuid] ASC)
);
GO
IF @@ERROR <> 0
    BEGIN
    ROLLBACK;
    END
ELSE
    BEGIN
    COMMIT TRANSACTION
    END
GO