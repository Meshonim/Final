using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Entities
{
    public class ProfileEntity
    {
        public ProfileEntity()
        {
            Lots = new HashSet<LotEntity>();
            Bids = new HashSet<BidEntity>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<LotEntity> Lots { get; set; }
        public ICollection<BidEntity> Bids { get; set; }

    }
}
