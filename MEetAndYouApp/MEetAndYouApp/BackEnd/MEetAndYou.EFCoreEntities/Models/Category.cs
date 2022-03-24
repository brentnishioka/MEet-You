using System;
using System.Collections.Generic;

namespace MEetAndYou.EFCoreEntities.Models
{
    public partial class Category
    {
        public Category()
        {
            Events = new HashSet<Event>();
        }

        public string CategoryName { get; set; } = null!;

        public virtual ICollection<Event> Events { get; set; }
    }
}
