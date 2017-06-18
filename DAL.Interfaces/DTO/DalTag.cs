using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces.DTO
{
    public class DalTag: IEntity
    {
        public DalTag()
        {
            Lots = new List<DalLot>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<DalLot> Lots { get; set; }
    }
}
