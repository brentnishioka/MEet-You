using System;

namespace Pentaskilled.MEetAndYou.Entities.DBModels
{
    public partial class UserToken
    {
        public int UserId { get; set; }
        public byte[] Token { get; set; }
        public Guid? Salt { get; set; }
        public string DateCreated { get; set; }

        public virtual UserAccountRecord User { get; set; }
    }
}
