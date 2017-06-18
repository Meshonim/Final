using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.Jobs
{
    using BLL.Interfaces.Services;
    using Quartz;
    using Quartz.Impl;
    using System.Diagnostics;

    namespace QuartzApp.Jobs
    {
        public class EmailScheduler
        {
            public static IScheduler Scheduler
            {
                get { return (IScheduler)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IScheduler)); }
            }

            public static void Start()
            {
                //IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
                Scheduler.Start();

                IJobDetail job = JobBuilder.Create<EmailSender>().Build();

                ITrigger trigger = TriggerBuilder.Create()  
                    .WithIdentity("trigger1", "group1")     
                    .StartNow()                            
                    .WithSimpleSchedule(x => x            
                        .WithIntervalInMinutes(1)          
                        .RepeatForever())                   
                    .Build();                               

                Scheduler.ScheduleJob(job, trigger);        
            }
        }
    }
}