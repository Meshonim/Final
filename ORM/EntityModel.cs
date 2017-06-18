using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM.Models;
using ORM.Config;
using System.Diagnostics;
using ILogger;

namespace ORM
{
    public class EntityModel: DbContext
    {
        private readonly ILog log;

        static EntityModel()
        {
            Database.SetInitializer<EntityModel>(new DbInitializer());
        }

        public EntityModel()
            : base("DefaultConnection")
        {
        }

        public EntityModel(ILog log): base("DefaultConnection")
        {
            this.log = log;
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Lot> Lots { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Bid> Bids { get; set; }

        public DbSet<Profile> Profiles { get; set; }

        public DbSet<ExceptionObject> ExceptionObjects { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfigutation());
            modelBuilder.Configurations.Add(new LotConfigutation());
            modelBuilder.Configurations.Add(new BidConfigutation());
            base.OnModelCreating(modelBuilder);         
        }

        public override int SaveChanges()
        {
            if (log != null)
            {
                log.Trace("DbContext: SaveChanges");
            }
            
            var list = Bids
            .Where(r => r.ProfileId == null)
            .ToList();

            list.ForEach(r => Bids.Remove(r));          
            return base.SaveChanges();
        }
    }      


       
}
