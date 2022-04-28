namespace MEetAndYou.EFCoreEntities.Models
{
    public partial class AdminAccountRecord
    {
        public int AdminId { get; set; }
        public string AdminEmail { get; set; } = null!;
        public byte[] AdminPassword { get; set; } = null!;
        public Guid? Salt { get; set; }
    }
}
