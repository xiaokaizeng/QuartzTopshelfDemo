using Quartz;
using SeanLibrary;
using System.Threading.Tasks;

namespace TimedTaskDemo
{
    class MyJob : IJob
    {

        async Task IJob.Execute(IJobExecutionContext context)
        {
            Log4NetHelper.Info("job excute");
        }
    }
}
