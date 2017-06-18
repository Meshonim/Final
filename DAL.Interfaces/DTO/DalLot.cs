using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces.DTO
{
    public class DalLot: IEntity
    {
        public DalLot()
        {
            Tags = new HashSet<DalTag>();
            Bids = new HashSet<DalBid>();
        }

        public int Id { get; set; }
        public bool IsChecked { get; set; }
        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }

        public byte[] Picture { get; set; }

        public virtual ICollection<DalTag> Tags { get; set; }
        public virtual ICollection<DalBid> Bids { get; set; }

        public int ProfileId { get; set; }
    }
}
