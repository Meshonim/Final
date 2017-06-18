using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces.DTO
{
    public class DalProfile: IEntity
    {
        public DalProfile()
        {
            Lots = new HashSet<DalLot>();
            Bids = new HashSet<DalBid>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<DalLot> Lots { get; set; }
        public virtual ICollection<DalBid> Bids { get; set; }

    }
}
