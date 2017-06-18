using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.Models
{
    public class Lot
    {
        public Lot()
        {
            Tags = new HashSet<Tag>();
            Bids = new HashSet<Bid>();
        }

        public int Id { get; set; }
        public bool IsChecked { get; set; }
        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }

        public byte[] Picture { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Bid> Bids { get; set; }
        public int ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
    }
}
