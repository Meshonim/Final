using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.Models
{
    public class Tag
    {
        public Tag()
        {
            Lots = new List<Lot>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Lot> Lots { get; set; }
    }
}
