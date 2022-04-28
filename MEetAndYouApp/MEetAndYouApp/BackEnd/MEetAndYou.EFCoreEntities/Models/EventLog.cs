namespace MEetAndYou.EFCoreEntities.Models
{
    public partial class EventLog
    {
        public int LogId { get; set; }
        public DateTime DateTime { get; set; }
        public string Category { get; set; } = null!;
        public string LogLevel { get; set; } = null!;
        public int UserId { get; set; }
        public string Message { get; set; } = null!;
    }
}
