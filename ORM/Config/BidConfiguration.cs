using ORM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.Config
{
    public class BidConfigutation : EntityTypeConfiguration<Bid>
    {
        public BidConfigutation()
        {
            Property(d => d.Date).IsRequired();
            Property(d => d.Price).IsRequired();
        }
    }
}
