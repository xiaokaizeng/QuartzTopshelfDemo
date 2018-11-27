using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using SeanLibrary;
using System;
using System.Threading.Tasks;
using Topshelf;

namespace TimedTaskDemo
{
    class QuartzServiceRunner : ServiceControl, ServiceSuspend
    {
        static IScheduler scheduler;

        //public async void Start()
        //{
        //    SeanLibrary.Log4NetHelper.Info("Start");
        //    RunProgramRunExample().GetAwaiter().GetResult();
        //}

        //public void Stop()
        //{
        //    scheduler.Clear();
        //}
        public bool Continue(HostControl hostControl)
        {
            scheduler.ResumeAll();
            Log4NetHelper.Info("所有调度作业重新开始");
            return true;
        }

        public bool Pause(HostControl hostControl)
        {
            scheduler.PauseAll();
            Log4NetHelper.Info("暂停所有调度作业");
            return true;
        }

        public bool Start(HostControl hostControl)
        {
            LogProvider.SetCurrentLogProvider(new ConsoleLogProvider());
            RunProgramRunExample().GetAwaiter().GetResult();
            Log4NetHelper.Info("开始作业调度");
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            scheduler.Clear();
            Log4NetHelper.Info("停止调度作业");
            return true;
        }

        private static async Task RunProgramRunExample()
        {
            try
            {
                // Grab the Scheduler instance from the Factory
                //NameValueCollection props = new NameValueCollection
                //{
                //    { "quartz.serializer.type", "binary" }
                //};
                //StdSchedulerFactory factory = new StdSchedulerFactory(props);
                StdSchedulerFactory factory = new StdSchedulerFactory();
                scheduler = await factory.GetScheduler();

                // and start it off
                await scheduler.Start();

                // define the job and tie it to our HelloJob class
                //IJobDetail job = JobBuilder.Create<MyJob>()
                //    .WithIdentity("job1", "group1")
                //    .Build();

                //// Trigger the job to run now, and then repeat every 10 seconds
                //ITrigger trigger = TriggerBuilder.Create()
                //    .WithIdentity("trigger1", "group1")
                //    .StartNow()
                //    .WithSimpleSchedule(x => x
                //        .WithIntervalInSeconds(10)
                //        .RepeatForever())
                //    .Build();

                //// Tell quartz to schedule the job using our trigger
                //await scheduler.ScheduleJob(job, trigger);
            }
            catch (SchedulerException se)
            {
                Log4NetHelper.Error(se.Message);
            }
        }// simple log provider to get something to the console
        private class ConsoleLogProvider : ILogProvider
        {
            public Logger GetLogger(string name)
            {
                return (level, func, exception, parameters) =>
                {
                    if (level >= LogLevel.Info && func != null)
                    {
                        Console.WriteLine("[" + DateTime.Now.ToLongTimeString() + "] [" + level + "] " + func(), parameters);
                    }
                    return true;
                };
            }

            public IDisposable OpenNestedContext(string message)
            {
                throw new NotImplementedException();
            }

            public IDisposable OpenMappedContext(string key, string value)
            {
                throw new NotImplementedException();
            }
        }
    }
}
