using DAL.Concrete;
using DAL.Interfaces.Interface;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM;
using DAL.Interfaces.DTO;
using BLL.Interfaces.Services;
using BLL;
using System.Data.Entity;
using Quartz;
using Quartz.Impl;
using Logger1;
using NLog;
using ILogger;

namespace DependencyResolver
{
    public static class ResolverConfig
    {
        public static void ConfigurateResolverWeb(this IKernel kernel)
        {
            Configure(kernel, true);
        }

        public static void ConfigurateResolverConsole(this IKernel kernel)
        {
            Configure(kernel, false);
        }

        private static void Configure(IKernel kernel, bool isWeb)
        {
            if (isWeb)
            {
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
                kernel.Bind<DbContext>().To<EntityModel>().InRequestScope();        
            }
            else
            {
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
                kernel.Bind<DbContext>().To<EntityModel>().InSingletonScope();
            }

            kernel.Bind<ILog>().ToMethod(x =>
            {
                var log = (ILog)LogManager.GetLogger((x.Request.ParentContext == null ?
                                  typeof(object).ToString() :
                                 x.Request.ParentContext.Plan.Type.ToString()), typeof(Log));
                return log;
            });

            kernel.Bind<IScheduler>().ToMethod(x =>
            {
                var sched = new StdSchedulerFactory().GetScheduler();
                sched.JobFactory = new NinjectJobFactory(kernel);
                return sched;
            }).InSingletonScope();

            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IRepository<DalUser>>().To<UserRepository>();

            kernel.Bind<IRoleService>().To<RoleService>();
            kernel.Bind<IRepository<DalRole>>().To<RoleRepository>();

            kernel.Bind<IBidService>().To<BidService>();
            kernel.Bind<IRepository<DalBid>>().To<BidRepository>();

            kernel.Bind<IProfileService>().To<ProfileService>();
            kernel.Bind<IRepository<DalProfile>>().To<ProfileRepository>();

            kernel.Bind<ILotService>().To<LotService>();
            kernel.Bind<IRepository<DalLot>>().To<LotRepository>();

            kernel.Bind<ITagService>().To<TagService>();
            kernel.Bind<IRepository<DalTag>>().To<TagRepository>();

            kernel.Bind<IExceptionObjectService>().To<ExceptionObjectService>();
            kernel.Bind<IRepository<DalExceptionObject>>().To<ExceptionObjectRepository>();
        }
    }
}
