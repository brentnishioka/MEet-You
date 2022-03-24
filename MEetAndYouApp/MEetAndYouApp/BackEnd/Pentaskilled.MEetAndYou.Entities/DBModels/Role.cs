using System;
using System.Collections.Generic;

namespace Pentaskilled.MEetAndYou.Entities.DBModels
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<UserAccountRecord>();
        }

        public string Role1 { get; set; }

        public virtual ICollection<UserAccountRecord> Users { get; set; }
    }
}
