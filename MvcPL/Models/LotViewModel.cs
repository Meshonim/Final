using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class LotViewModel
    {
        public LotViewModel()
        {
            Tags = new HashSet<TagViewModel>();
        }

        public int Id { get; set; }
        public bool IsChecked { get; set; }
        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }

        public byte[] Picture { get; set; }

        public virtual ICollection<TagViewModel> Tags { get; set; }

        public int ProfileId { get; set; }
    }
}