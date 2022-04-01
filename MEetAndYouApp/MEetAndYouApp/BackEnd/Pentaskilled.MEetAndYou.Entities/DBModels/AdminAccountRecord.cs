using System;
using System.Collections.Generic;

namespace Pentaskilled.MEetAndYou.Entities.DBModels
{
    public partial class AdminAccountRecord
    {
        public int AdminId { get; set; }
        public string AdminEmail { get; set; }
        public byte[] AdminPassword { get; set; }
        public Guid? Salt { get; set; }
    }
}
