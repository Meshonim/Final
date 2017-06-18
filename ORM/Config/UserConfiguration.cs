using ORM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.Config
{
    public class UserConfigutation : EntityTypeConfiguration<User>
    {
        public UserConfigutation()
        {
            Property(d => d.Email).IsRequired();
        }
    }
}
