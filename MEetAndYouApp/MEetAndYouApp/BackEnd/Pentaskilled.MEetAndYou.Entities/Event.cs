using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentaskilled.MEetAndYou.Entities
{
    public class Event
    {
        private float _price;
        public int EventID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public float Price {
            get { return _price; }
            set {
                if (value >= 0)
                {
                    _price = value;
                }
            }
        }
        public DateTime Date { get; set; }

        // Default Constructor for Event
        public Event()
        {

        }

        public Event (int eventID, string name, string description, string address, float price, DateTime date)
        {
            EventID = eventID;
            Name = name;
            Description = description;
            Address = address;
            Price = price;
            Date = date;
        }
    }
}
