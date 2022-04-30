using System.Collections.Generic;

namespace Pentaskilled.MEetAndYou.Entities.DBModels
{
    public partial class Category
    {
        public Category()
        {
            Events = new HashSet<Event>();
        }

        public string CategoryName { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
