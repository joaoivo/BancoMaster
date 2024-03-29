CREATE DATABASE [BancoMaster]
 CONTAINMENT = NONE
 ON  PRIMARY ( NAME = N'BancoMaster', FILENAME = N'/var/opt/mssql/data/BancoMaster.mdf' , SIZE = 8192KB , FILEGROWTH = 65536KB )
 --LOG ON ( NAME = N'BancoMaster_log', FILENAME = N'/var/opt/mssql/data/BancoMaster_log.ldf' , SIZE = 8192KB , FILEGROWTH = 65536KB )
 COLLATE SQL_Latin1_General_CP1_CI_AS
GO
ALTER DATABASE [BancoMaster] SET COMPATIBILITY_LEVEL = 150
GO
ALTER DATABASE [BancoMaster] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BancoMaster] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BancoMaster] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BancoMaster] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BancoMaster] SET ARITHABORT OFF 
GO
ALTER DATABASE [BancoMaster] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BancoMaster] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BancoMaster] SET AUTO_CREATE_STATISTICS OFF
GO
ALTER DATABASE [BancoMaster] SET AUTO_UPDATE_STATISTICS OFF 
GO
ALTER DATABASE [BancoMaster] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BancoMaster] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BancoMaster] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BancoMaster] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BancoMaster] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BancoMaster] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BancoMaster] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BancoMaster] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BancoMaster] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BancoMaster] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BancoMaster] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BancoMaster] SET  READ_WRITE 
GO
ALTER DATABASE [BancoMaster] SET RECOVERY FULL 
GO
ALTER DATABASE [BancoMaster] SET  MULTI_USER 
GO
ALTER DATABASE [BancoMaster] SET PAGE_VERIFY TORN_PAGE_DETECTION  
GO
ALTER DATABASE [BancoMaster] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BancoMaster] SET DELAYED_DURABILITY = DISABLED 
GO
USE [BancoMaster]
GO
IF NOT EXISTS (SELECT name FROM sys.filegroups WHERE is_default=1 AND name = N'PRIMARY') ALTER DATABASE [BancoMaster] MODIFY FILEGROUP [PRIMARY] DEFAULT
GO
