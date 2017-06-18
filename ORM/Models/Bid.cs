using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.Models
{
    public class Bid
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Price { get; set; }
        public int? ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
        public int LotId { get; set; }
        public virtual Lot Lot { get; set; }
    }
}
