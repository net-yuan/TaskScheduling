using System;
using System.Collections.Specialized;
using System.Threading;

using Common.Logging;
using Quartz.Impl;
using Quartz;

namespace TaskScheduling
{
    public class RemoteServerExample
    {
        public static void Run()
        {
            //ILog log = LogManager.GetLogger(typeof(RemoteServerExample));

            NameValueCollection properties = new NameValueCollection();
            properties["quartz.scheduler.instanceName"] = "RemoteAutoTaskServer";

            properties["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz";
            properties["quartz.threadPool.threadCount"] = "5";
            properties["quartz.threadPool.threadPriority"] = "Normal";

            // 设置服务器
            properties["quartz.scheduler.exporter.type"] = "Quartz.Simpl.RemotingSchedulerExporter, Quartz";
            properties["quartz.scheduler.exporter.port"] = "555";
            properties["quartz.scheduler.exporter.bindName"] = "QuartzScheduler";
            properties["quartz.scheduler.exporter.channelType"] = "tcp";

            #region 2.3.0版本可用

            //properties["quartz.scheduler.exporter.channelName"] = "httpQuartz";
            // 拒绝远程
            //properties["quartz.scheduler.exporter.rejectRemoteRequests"] = "true";

            #endregion 2.3.0版本可用

            #region 持久化所用

            //存储类型
            //properties["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz";
            ////表明前缀
            //properties["quartz.jobStore.tablePrefix"] = "QRTZ_";
            ////驱动类型
            //properties["quartz.jobStore.driverDelegateType"] = "Quartz.Impl.AdoJobStore.SqlServerDelegate, Quartz";
            ////数据源名称
            //properties["quartz.jobStore.dataSource"] = "myDS";
            ////连接字符串
            //properties["quartz.dataSource.myDS.connectionString"] = @"Data Source=(local);Initial Catalog=quartz;User ID=sa;Password=Ayy123";
            ////sqlserver版本
            //properties["quartz.dataSource.myDS.provider"] = "SqlServer-20";

            #endregion 持久化所用

            ISchedulerFactory sf = new StdSchedulerFactory(properties);
            IScheduler sched = sf.GetScheduler();
            sched.Start();

            SchedulerMetaData metaData = sched.GetMetaData();

            IJobDetail job = JobBuilder.Create<SimpleJob>()
                .WithIdentity("remotelyAddedJob", "default")
                .Build();

            JobDataMap map = job.JobDataMap;
            map.Put("msg", "信息");

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("remotelyAddedTrigger", "default")
                .ForJob(job.Key)
                .WithCronSchedule("/5 * * ? * *")
                .Build();

            string name = sched.SchedulerName;
            sched.ScheduleJob(job, trigger);
        }
    }
}