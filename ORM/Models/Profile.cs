using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.Models
{
    public class Profile
    {
        public Profile()
        {
            Lots = new HashSet<Lot>();
            Bids = new HashSet<Bid>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Lot> Lots { get; set; }
        public virtual ICollection<Bid> Bids { get; set; }

    }
}
