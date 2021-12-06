-- System and user event logs in a combined table.
--CREATE TABLE [dbo].[ApplicationLogs]
--(
--	  [LogId] INT NOT NULL PRIMARY KEY,
--	  [LogType] VARCHAR(10) NOT NULL, -- Indicates whether the log is a system or a user log.
--	  [DateTime] DATETIME NOT NULL,
--    [Category] VARCHAR(15) NOT NULL,
--    [LogLevel] VARCHAR(15) NOT NULL,
--    [UserId] INT NOT NULL, -- If the LogType is a system log, then the user ID will be 0, representing the system.
--    [Message] VARCHAR(255) NOT NULL
--)

-- All user event logs in a single table.
CREATE TABLE [dbo].[UserEventLogs]
(
	[LogId] INT NOT NULL PRIMARY KEY,
	[DateTime] DATETIME NOT NULL,
	[Category] VARCHAR(15) NOT NULL,
	[LogLevel] VARCHAR(15) NOT NULL,
	[UserId] INT NOT NULL,
	[Message] VARCHAR(255) NOT NULL
)

-- All system logs in a single table.
CREATE TABLE [dbo].[SystemEventLogs]
(
	[LogId] INT NOT NULL PRIMARY KEY,
	[DateTime] DATETIME NOT NULL,
	[Category] VARCHAR(15) NOT NULL,
	[LogLevel] VARCHAR(15) NOT NULL,
	[Message] VARCHAR(255) NOT NULL
)