using System.Collections.Specialized;

using Common.Logging;

using Quartz.Impl;
using Quartz;

namespace TaskScheduling
{
    public class RemoteClientExample
    {
        public static void Run()
        {
            ILog log = LogManager.GetLogger(typeof(RemoteClientExample));

            NameValueCollection properties = new NameValueCollection();
            properties["quartz.scheduler.instanceName"] = "RemoteClient";

            // 设置线程池
            properties["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz";
            properties["quartz.threadPool.threadCount"] = "5";
            properties["quartz.threadPool.threadPriority"] = "Normal";

            // 设置远程连接
            //properties["quartz.scheduler.proxy"] = "true";
            //properties["quartz.scheduler.proxy.address"] = "tcp://127.0.0.1:556/QuartzScheduler";

            ISchedulerFactory sf = new StdSchedulerFactory(properties);
            IScheduler scheduler = sf.GetScheduler("RemoteAutoTaskServer");

            IJobDetail job = JobBuilder.Create<SimpleJob>()
                .WithIdentity("remotelyAddedJob1", "default")
                .Build();

            JobDataMap map = job.JobDataMap;
            map.Put("msg", "信息");

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("remotelyAddedTrigger1", "default")
                .ForJob(job.Key)
                .WithCronSchedule("/5 * * ? * *")
                .Build();

            string name = scheduler.SchedulerName;
            scheduler.ScheduleJob(job, trigger);

            log.Info("向服务器添加计划任务");
        }

        public string Name
        {
            get { return null; }
        }
    }
}