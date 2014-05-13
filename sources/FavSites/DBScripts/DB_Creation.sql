USE [master]
GO

/****** Object:  Database [FavouriteSites]    Script Date: 21.10.2013 15:06:03 ******/
CREATE DATABASE [FavouriteSites]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FavouriteSites', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\FavouriteSites.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'FavouriteSites_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\FavouriteSites_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [FavouriteSites] SET COMPATIBILITY_LEVEL = 110
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FavouriteSites].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [FavouriteSites] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [FavouriteSites] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [FavouriteSites] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [FavouriteSites] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [FavouriteSites] SET ARITHABORT OFF 
GO

ALTER DATABASE [FavouriteSites] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [FavouriteSites] SET AUTO_CREATE_STATISTICS ON 
GO

ALTER DATABASE [FavouriteSites] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [FavouriteSites] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [FavouriteSites] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [FavouriteSites] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [FavouriteSites] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [FavouriteSites] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [FavouriteSites] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [FavouriteSites] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [FavouriteSites] SET  DISABLE_BROKER 
GO

ALTER DATABASE [FavouriteSites] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [FavouriteSites] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [FavouriteSites] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [FavouriteSites] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [FavouriteSites] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [FavouriteSites] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [FavouriteSites] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [FavouriteSites] SET RECOVERY FULL 
GO

ALTER DATABASE [FavouriteSites] SET  MULTI_USER 
GO

ALTER DATABASE [FavouriteSites] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [FavouriteSites] SET DB_CHAINING OFF 
GO

ALTER DATABASE [FavouriteSites] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [FavouriteSites] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO

ALTER DATABASE [FavouriteSites] SET  READ_WRITE 
GO


