using System;
using System.Collections.Generic;

namespace MEetAndYou.EFCoreEntities.Models
{
    public partial class UserToken
    {
        public int UserId { get; set; }
        public byte[] Token { get; set; } = null!;
        public Guid? Salt { get; set; }
        public string DateCreated { get; set; } = null!;

        public virtual UserAccountRecord User { get; set; } = null!;
    }
}
