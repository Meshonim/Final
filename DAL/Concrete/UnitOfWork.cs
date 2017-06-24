using DAL.Interfaces.Interface;
using ILogger;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; private set; }
        private readonly ILog log;

        public UnitOfWork(DbContext context, ILog log)
        {
            Context = context;
            this.log = log;
        }

        public void Commit()
        {
            log.Trace("Commit: start");
            if (Context != null)
            {
                try
                {
                    Context.SaveChanges();
                }
                catch (DbEntityValidationException ex)                   
                {
                    foreach (var eve in ex.EntityValidationErrors)
                    {
                        log.Error("Error: Entity (\"{0}\") in state \"{1}\" had the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var vle in eve.ValidationErrors)
                        {
                            log.Error("- PropertyName: \"{0}\", ErrorMessage: \"{1}\"",
                                vle.PropertyName, vle.ErrorMessage);
                        }
                    }
                    throw new ApplicationException("There was an error while working with database. Please see InnerException for more details", ex);
                }
                catch (DataException ex)
                {
                    log.Error("Error: DataException occured while commiting" + ex.ToString() );
                    throw new ApplicationException("There was an error while working with database. Please see InnerException for more details", ex);
                }
            }
            log.Trace("Commited successfully");
        }

        public void Dispose()
        {
            if (Context != null)
            {
                Context.Dispose();
            }
        }
    }
}
