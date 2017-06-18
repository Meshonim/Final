using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Entities
{
    public class LotEntity
    {
        public LotEntity()
        {
            Tags = new HashSet<TagEntity>();
            Bids = new HashSet<BidEntity>();
        }

        public int Id { get; set; }
        public bool IsChecked { get; set; }
        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }

        public byte[] Picture { get; set; }

        public virtual ICollection<TagEntity> Tags { get; set; }
        public virtual ICollection<BidEntity> Bids { get; set; }

        public int ProfileId { get; set; }
    }
}
