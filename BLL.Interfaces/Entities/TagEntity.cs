using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Entities
{
    public class TagEntity
    {
        public TagEntity()
        {
            Lots = new List<LotEntity>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<LotEntity> Lots { get; set; }
    }
}
