using ORM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.Config
{
    public class LotConfigutation : EntityTypeConfiguration<Lot>
    {
        public LotConfigutation()
        {
            Property(d => d.Name).IsRequired();
            Property(d => d.IsChecked).IsRequired();
            Property(d => d.IsActive).IsRequired();
        }
    }
}
