using Pentaskilled.MEetAndYou.Entities;

namespace Pentaskilled.MEetAndYou.Logging
{
    public interface ILoggingService
    {
        Task<bool> CreateNewLogAsync(DateTime dateTime, string category, LogLevel logLevel, string message);
        Task<bool> CreateNewLogAsync(DateTime dateTime, string category, LogLevel logLevel, int userId, string message);
        Task<Log> MakeLogAsync(DateTime dateTime, string category, LogLevel logLevel, string message);
        Task<Log> MakeLogAsync(DateTime dateTime, string category, LogLevel logLevel, int userId, string message);
        Task<bool> PushLogToDBAsync(Log eventLog);
    }
}
