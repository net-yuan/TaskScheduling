using System.Collections.Specialized;

using Common.Logging;

using Quartz.Impl;
using Quartz;

namespace TaskScheduling
{
    public class JobDel
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

            JobKey jk = new JobKey("remotelyAddedJob1", "default");
            scheduler.DeleteJob(jk);
            JobKey jk1 = new JobKey("remotelyAddedJob", "default");
            scheduler.DeleteJob(jk1);

            log.Info("向服务器删除计划任务");
        }

        public string Name
        {
            get { return null; }
        }
    }
}