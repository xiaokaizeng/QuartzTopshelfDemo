using SeanLibrary;
using System;
using System.Configuration;
using Topshelf;

namespace TimedTaskDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //var logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config");
            //log4net.Config.XmlConfigurator.ConfigureAndWatch(logCfg);

            var config = ConfigurationManager.GetSection("quartz");
            Console.WriteLine(config);

            HostFactory.Run(x =>
            {
                Log4NetHelper.Info("服务开始运行");
                HostFactory.Run(o =>

                {
                    //o.UseLog4Net(); //这里需要使用 log4net, Version=1.2.15.0 的版本，当前版本不兼容所以注释掉
                    o.Service<QuartzServiceRunner>();
                    o.SetServiceName("topshelf调度作业");
                    o.SetDisplayName("topshelf调度作业");
                    o.SetDescription("topshelf调度作业");
                    o.EnablePauseAndContinue();

                });
            });
        }
        //private static void Main(string[] args)
        //{
        //    SeanLibrary.Log4NetHelper.Info("Main");
        //    LogProvider.SetCurrentLogProvider(new ConsoleLogProvider());

        //    RunProgramRunExample().GetAwaiter().GetResult();

        //    SeanLibrary.Log4NetHelper.Info("Main1");
        //    Console.WriteLine("Press any key to close the application");
        //    Console.ReadKey();
        //}
    }

}
