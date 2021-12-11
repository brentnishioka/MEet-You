-- All user event logs in a single table.
CREATE TABLE [MEetAndYou].[UserEventLogs]
(
    [LogId] INT NOT NULL IDENTITY PRIMARY KEY,
    [DateTime] DATETIME NOT NULL,
    [Category] VARCHAR(15) NOT NULL,
    [LogLevel] VARCHAR(15) NOT NULL,
    [UserId] INT NOT NULL,
    [Message] VARCHAR(255) NOT NULL
)

-- All system logs in a single table.
CREATE TABLE [MEetAndYou].[SystemEventLogs]
(
    [LogId] INT NOT NULL IDENTITY PRIMARY KEY,
    [DateTime] DATETIME NOT NULL,
    [Category] VARCHAR(15) NOT NULL,
    [LogLevel] VARCHAR(15) NOT NULL,
    [Message] VARCHAR(255) NOT NULL
)

--DROP TABLE [MEetAndYou].[UserEventLogs]
--DROP TABLE [MEetAndYou].[SystemEventLogs]