using System;
using System.Collections.Generic;

namespace MEetAndYou.EFCoreEntities.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<UserAccountRecord>();
        }

        public string Role1 { get; set; } = null!;

        public virtual ICollection<UserAccountRecord> Users { get; set; }
    }
}
