USE [master]
GO
/****** Object:  Database [MEetAndYou-DB]    Script Date: 3/5/2022 4:13:50 PM ******/
CREATE DATABASE [MEetAndYou-DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MEetAndYou-DB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\MEetAndYou-DB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MEetAndYou-DB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\MEetAndYou-DB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [MEetAndYou-DB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MEetAndYou-DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MEetAndYou-DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MEetAndYou-DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MEetAndYou-DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MEetAndYou-DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MEetAndYou-DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [MEetAndYou-DB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MEetAndYou-DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MEetAndYou-DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MEetAndYou-DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MEetAndYou-DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MEetAndYou-DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MEetAndYou-DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MEetAndYou-DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MEetAndYou-DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MEetAndYou-DB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MEetAndYou-DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MEetAndYou-DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MEetAndYou-DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MEetAndYou-DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MEetAndYou-DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MEetAndYou-DB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MEetAndYou-DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MEetAndYou-DB] SET RECOVERY FULL 
GO
ALTER DATABASE [MEetAndYou-DB] SET  MULTI_USER 
GO
ALTER DATABASE [MEetAndYou-DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MEetAndYou-DB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MEetAndYou-DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MEetAndYou-DB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MEetAndYou-DB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MEetAndYou-DB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'MEetAndYou-DB', N'ON'
GO
ALTER DATABASE [MEetAndYou-DB] SET QUERY_STORE = OFF
GO
USE [MEetAndYou-DB]
GO
/****** Object:  User [MEetYouWriteUserLogin]    Script Date: 3/5/2022 4:13:50 PM ******/
CREATE USER [MEetYouWriteUserLogin] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [MEetYouReadUserLogin]    Script Date: 3/5/2022 4:13:50 PM ******/
CREATE USER [MEetYouReadUserLogin] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [MEetAndYouDBUser]    Script Date: 3/5/2022 4:13:50 PM ******/
CREATE USER [MEetAndYouDBUser] FOR LOGIN [MEetAndYouDBUser] WITH DEFAULT_SCHEMA=[MEetAndYou]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [MEetYouWriteUserLogin]
GO
ALTER ROLE [db_datareader] ADD MEMBER [MEetYouReadUserLogin]
GO
ALTER ROLE [db_datareader] ADD MEMBER [MEetAndYouDBUser]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [MEetAndYouDBUser]
GO
/****** Object:  Schema [MEetAndYou]    Script Date: 3/5/2022 4:13:50 PM ******/
CREATE SCHEMA [MEetAndYou]
GO
/****** Object:  UserDefinedFunction [MEetAndYou].[CheckExistingLog]    Script Date: 3/5/2022 4:13:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [MEetAndYou].[CheckExistingLog]
(
    -- Add the parameters for the function here
    @logId int
)
RETURNS INT
AS
BEGIN
    -- Declare the return variable here
    DECLARE @rowCount INT;

    -- Add the T-SQL statements to compute the return value here
    SET @rowCount = (SELECT COUNT(LogId) FROM [MEetAndYou].[EventLogs] WHERE @logId = EventLogs.LogId);

    -- Return the result of the function
    RETURN @rowCount
END
GO
/****** Object:  UserDefinedFunction [MEetAndYou].[GetArchiveCount]    Script Date: 3/5/2022 4:13:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [MEetAndYou].[GetArchiveCount] 
(

)
RETURNS int 
AS
BEGIN
    -- Declare the return variable here
    DECLARE @logArchiveCount int;

    -- Add the T-SQL statements to compute the return value here
    SET @logArchiveCount = (SELECT COUNT(*) FROM EventLogs WHERE DATEDIFF(day, DateTime, GETDATE()) >= 30)

    -- Return the result of the function
    RETURN @logArchiveCount;

END
GO
/****** Object:  UserDefinedFunction [MEetAndYou].[GetCurrentIdentity]    Script Date: 3/5/2022 4:13:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [MEetAndYou].[GetCurrentIdentity]
(

)
RETURNS INT
AS
BEGIN
	-- Declare the return variable here
	DECLARE @lastId INT;

	SET @lastId = (SELECT IDENT_CURRENT('MEetAndYou.EventLogs'));

	-- Return the result of the function
	RETURN @lastId
END
GO
/****** Object:  UserDefinedFunction [MEetAndYou].[ValidateCredentialsInDB]    Script Date: 3/5/2022 4:13:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [MEetAndYou].[ValidateCredentialsInDB] (
    @email VARCHAR(30),
    @password VARCHAR(15)
)
RETURNS INT
AS
BEGIN
    DECLARE @result INT
    DECLARE @salt uniqueidentifier

    SELECT @salt = [Salt] FROM [MEetAndYou].[UserAccountRecords]
    WHERE [UserEmail] = @email

    SELECT @result = COUNT([UserID]) FROM [MEetAndYou].[UserAccountRecords] 
    WHERE [UserEmail] = @email
    AND [UserPassword] = HASHBYTES('SHA2_512', @password+CAST(@salt AS nvarchar(36)))
    return @result
END
GO
/****** Object:  UserDefinedFunction [MEetAndYou].[VerifyAdminRecordInDB]    Script Date: 3/5/2022 4:13:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [MEetAndYou].[VerifyAdminRecordInDB] (
    @email VARCHAR(30),
    @password VARCHAR(15)
)
RETURNS INT
AS
BEGIN
    DECLARE @result INT
    DECLARE @salt uniqueidentifier

    SELECT @salt = [Salt] FROM [MEetAndYou].[AdminAccountRecords]
    WHERE [AdminEmail] = @email

    SELECT @result = COUNT([AdminID]) FROM [MEetAndYou].[AdminAccountRecords] 
    WHERE [AdminEmail] = @email
    AND [AdminPassword] = HASHBYTES('SHA2_512', @password+CAST(@salt AS nvarchar(36)))
    return @result
END
GO
/****** Object:  UserDefinedFunction [MEetAndYou].[VerifyUserRecordInDB]    Script Date: 3/5/2022 4:13:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [MEetAndYou].[VerifyUserRecordInDB] (
    @id INT,
    @email VARCHAR(30),
    @password VARCHAR(15), 
    @phoneNum VARCHAR(15), 
    @registerDate DateTime, 
    @active BIT
)
RETURNS INT
AS
BEGIN
    DECLARE @result INT
    DECLARE @salt uniqueidentifier

    SELECT @salt = [Salt] FROM [MEetAndYou].[UserAccountRecords]
    WHERE [UserId] = @id

    SELECT @result = COUNT([UserID]) FROM [MEetAndYou].[UserAccountRecords] 
    WHERE [UserID] = @id
    AND [UserEmail] = @email
    AND [UserPassword] = HASHBYTES('SHA2_512', @password+CAST(@salt AS nvarchar(36)))
    AND [UserPhoneNum] = @phoneNum
    AND [UserRegisterDate] = @registerDate
    AND [Active] = @active
    return @result
END
GO
/****** Object:  Table [MEetAndYou].[EventLogs]    Script Date: 3/5/2022 4:13:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [MEetAndYou].[EventLogs](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[Category] [varchar](15) NOT NULL,
	[LogLevel] [varchar](15) NOT NULL,
	[UserId] [int] NOT NULL,
	[Message] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [MEetAndYou].[Logs30DaysOld]    Script Date: 3/5/2022 4:13:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:        <Author,,Name>
-- Create date: <Create Date,,>
-- Description:    <Description,,>
-- =============================================
CREATE FUNCTION [MEetAndYou].[Logs30DaysOld]
(    
    -- Add the parameters for the function here

)
RETURNS TABLE 
AS
RETURN 
(
    -- Add the SELECT statement with parameter references here
    SELECT  * FROM EventLogs WHERE DATEDIFF(day, DateTime, GETDATE()) >= 30
)
GO
/****** Object:  Table [MEetAndYou].[AdminAccountRecords]    Script Date: 3/5/2022 4:13:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [MEetAndYou].[AdminAccountRecords](
	[AdminID] [int] IDENTITY(1,1) NOT NULL,
	[AdminEmail] [varchar](30) NOT NULL,
	[AdminPassword] [binary](64) NOT NULL,
	[Salt] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[AdminID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [MEetAndYou].[UserAccountRecords]    Script Date: 3/5/2022 4:13:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [MEetAndYou].[UserAccountRecords](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserEmail] [varchar](30) NULL,
	[UserPassword] [binary](64) NOT NULL,
	[UserPhoneNum] [varchar](15) NOT NULL,
	[UserRegisterDate] [varchar](30) NOT NULL,
	[Active] [bit] NOT NULL,
	[Salt] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [MEetAndYou].[ArchiveDelete]    Script Date: 3/5/2022 4:13:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:        <Author,,Name>
-- Create date: <Create Date,,>
-- Description:    <Description,,>
-- =============================================
CREATE PROCEDURE [MEetAndYou].[ArchiveDelete]
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    -- Insert statements for procedure here
    DELETE FROM EventLogs WHERE DATEDIFF(day, DateTime, GETDATE()) >= 30;
END
GO
/****** Object:  StoredProcedure [MEetAndYou].[CreateAdminAccountRecord]    Script Date: 3/5/2022 4:13:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [MEetAndYou].[CreateAdminAccountRecord]
    -- Add the parameters for the stored procedure here
    @email VARCHAR(30),
    @password VARCHAR(15)
AS
    DECLARE @salt UNIQUEIDENTIFIER = NEWID()

    -- Insert statements for procedure here
    INSERT INTO [MEetAndYou].[AdminAccountRecords] ([AdminEmail], [AdminPassword], [Salt])
    VALUES (@email,
            HASHBYTES('SHA2_512', @password+CAST(@salt AS nvarchar(36))),
            @salt)
GO
/****** Object:  StoredProcedure [MEetAndYou].[CreateUserAccountRecord]    Script Date: 3/5/2022 4:13:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [MEetAndYou].[CreateUserAccountRecord]
    -- Add the parameters for the stored procedure here
    @email VARCHAR(30),
    @password VARCHAR(15),
    @phoneNum VARCHAR(15),
    @registerDate VARCHAR(30),
    @active BIT
AS
    DECLARE @salt UNIQUEIDENTIFIER = NEWID()

    -- Insert statements for procedure here
    INSERT INTO [MEetAndYou].[UserAccountRecords] ([UserEmail], [UserPassword], [UserPhoneNum], [UserRegisterDate], [Active], [Salt])
    VALUES (@email,
            HASHBYTES('SHA2_512', @password+CAST(@salt AS nvarchar(36))),
            @phoneNum,
            @registerDate,
            @active,
            @salt)
GO
/****** Object:  StoredProcedure [MEetAndYou].[DeleteAdminAccountRecord]    Script Date: 3/5/2022 4:13:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [MEetAndYou].[DeleteAdminAccountRecord]
    -- Add the parameters for the stored procedure here
    @id INT
AS
    -- Delete statements for procedure here
    DELETE FROM [MEetAndYou].[AdminAccountRecords]
    WHERE [AdminID] = @id
GO
/****** Object:  StoredProcedure [MEetAndYou].[DeleteUserAccountRecord]    Script Date: 3/5/2022 4:13:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [MEetAndYou].[DeleteUserAccountRecord]
    -- Add the parameters for the stored procedure here
    @id INT
AS
    -- Delete statements for procedure here
    DELETE FROM [MEetAndYou].[UserAccountRecords]
    WHERE [UserID] = @id
GO
/****** Object:  StoredProcedure [MEetAndYou].[DisableUserAccountRecord]    Script Date: 3/5/2022 4:13:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [MEetAndYou].[DisableUserAccountRecord]
    -- Add the parameters for the stored procedure here
    @id INT
AS
    -- Update statements for procedure here
    UPDATE [MEetAndYou].[UserAccountRecords] 
    SET [Active] = 0
    WHERE [UserID] = @id
GO
/****** Object:  StoredProcedure [MEetAndYou].[EnableUserAccountRecord]    Script Date: 3/5/2022 4:13:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [MEetAndYou].[EnableUserAccountRecord]
    -- Add the parameters for the stored procedure here
    @id INT
AS
    -- Update statements for procedure here
    UPDATE [MEetAndYou].[UserAccountRecords] 
    SET [Active] = 1
    WHERE [UserID] = @id
GO
/****** Object:  StoredProcedure [MEetAndYou].[InsertLog]    Script Date: 3/5/2022 4:13:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [MEetAndYou].[InsertLog]
	-- Add the parameters for the stored procedure here
	@dateTime datetime,
	@category varchar(15),
	@logLevel varchar(15),
	@userId int,
	@message varchar(255)
AS
    -- Insert statements for procedure here
	INSERT INTO [MEetAndYou].[EventLogs] ([DateTime], [Category], [LogLevel], [UserId], [Message])
	VALUES (@dateTime, @category, @logLevel, @userId, @message)
GO
/****** Object:  StoredProcedure [MEetAndYou].[UpdateAdminAccountEmail]    Script Date: 3/5/2022 4:13:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [MEetAndYou].[UpdateAdminAccountEmail]
    -- Add the parameters for the stored procedure here
    @id INT,
    @email VARCHAR(30)
AS
    -- Update statements for procedure here
    UPDATE [MEetAndYou].[AdminAccountRecords] 
    SET [AdminEmail] = @email
    WHERE [AdminID] = @id
GO
/****** Object:  StoredProcedure [MEetAndYou].[UpdateAdminAccountPassword]    Script Date: 3/5/2022 4:13:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [MEetAndYou].[UpdateAdminAccountPassword]
    -- Add the parameters for the stored procedure here
    @id INT,
    @password VARCHAR(15)
AS
    DECLARE @salt UNIQUEIDENTIFIER = NEWID()

    -- Update statements for procedure here
    UPDATE [MEetAndYou].[AdminAccountRecords] 
    SET 
    [AdminPassword] = HASHBYTES('SHA2_512', @password+CAST(@salt AS nvarchar(36))),
    [Salt] = @salt
    WHERE [AdminID] = @id
GO
/****** Object:  StoredProcedure [MEetAndYou].[UpdateLog]    Script Date: 3/5/2022 4:13:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:        <Author,,Name>
-- Create date: <Create Date,,>
-- Description:    <Description,,>
-- =============================================
CREATE PROCEDURE [MEetAndYou].[UpdateLog]
(
    @logId int
)
AS
BEGIN
    UPDATE MEetAndYou.EventLogs
    SET DateTime = GETUTCDATE()
    WHERE LogId = @logId
END
GO
/****** Object:  StoredProcedure [MEetAndYou].[UpdateUserAccountEmail]    Script Date: 3/5/2022 4:13:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [MEetAndYou].[UpdateUserAccountEmail]
    -- Add the parameters for the stored procedure here
    @id INT,
    @email VARCHAR(30)
AS
    -- Update statements for procedure here
    UPDATE [MEetAndYou].[UserAccountRecords] 
    SET [UserEmail] = @email
    WHERE [UserID] = @id
GO
/****** Object:  StoredProcedure [MEetAndYou].[UpdateUserAccountPassword]    Script Date: 3/5/2022 4:13:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [MEetAndYou].[UpdateUserAccountPassword]
    -- Add the parameters for the stored procedure here
    @id INT,
    @password VARCHAR(15)
AS
    DECLARE @salt UNIQUEIDENTIFIER = NEWID()

    -- Update statements for procedure here
    UPDATE [MEetAndYou].[UserAccountRecords] 
    SET 
    [UserPassword] = HASHBYTES('SHA2_512', @password+CAST(@salt AS nvarchar(36))),
    [Salt] = @salt
    WHERE [UserID] = @id
GO
/****** Object:  StoredProcedure [MEetAndYou].[UpdateUserAccountPhoneNum]    Script Date: 3/5/2022 4:13:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [MEetAndYou].[UpdateUserAccountPhoneNum]
    -- Add the parameters for the stored procedure here
    @id INT,
    @phoneNum VARCHAR(15)
AS
    -- Update statements for procedure here
    UPDATE [MEetAndYou].[UserAccountRecords] 
    SET [UserPhoneNum] = @phoneNum
    WHERE [UserID] = @id
GO
USE [master]
GO
ALTER DATABASE [MEetAndYou-DB] SET  READ_WRITE 
GO
