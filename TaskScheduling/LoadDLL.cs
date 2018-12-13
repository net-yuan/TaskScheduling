using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using System.Collections.Specialized;
using Quartz.Impl;
using System.IO;
using System.Reflection;

namespace TaskScheduling
{
    public class LoadDLL
    {
        public static void Run()
        {
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
            IScheduler scheduler = sf.GetScheduler();

            //IJobDetail job = JobBuilder.Create<SimpleJob>()
            //    .WithIdentity("remotelyAddedJob1", "default")
            //    .Build();

            //JobDataMap map = job.JobDataMap;
            //map.Put("msg", "信息");

            //ITrigger trigger = TriggerBuilder.Create()
            //    .WithIdentity("remotelyAddedTrigger1", "default")
            //    .ForJob(job.Key)
            //    .WithCronSchedule("/5 * * ? * *")
            //    .Build();

            //string name = scheduler.SchedulerName;
            //scheduler.ScheduleJob(job, trigger);

            //先上传，上传完成之后执行反射读取其中Job，添加任务
            //@"D:\用户目录\Downloads\SourceCode1\RemoteScheduler\ConsoleApplication2\bin\Debug\ConsoleApplication1.exe"
            string path = System.Web.HttpContext.Current.Server.MapPath("/AutoTaskDLL/AutoTaskConsole.exe");
            if (File.Exists(path))
            {
                System.Reflection.Assembly ass = Assembly.LoadFrom(path); //加载DLL

                Type[] tmd = ass.GetTypes();
                for (int i = 0; i < tmd.Length; i++)
                {
                    //必须以AutoJob结尾
                    if (tmd[i].Name.Contains("RemoteClientExample"))
                    {
                        //    Console.WriteLine(tmd[i].Name);

                        //    var job = JobBuilder.Create(tmd[i])
                        //.WithIdentity(tmd[i].Name, "分组一")
                        //.Build();

                        //    var trigger = TriggerBuilder.Create()
                        //        .WithIdentity("myJobTrigger" + tmd[i].Name, "分组一")
                        //        .StartNow()
                        //        .WithCronSchedule("/10 * * ? * *")
                        //        .Build();
                        //    scheduler.ScheduleJob(job, trigger);
                        //scheduler.Start();

                        MethodInfo[] finfos = tmd[i].GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Static);
                        foreach (MethodInfo finfo in finfos)
                        {
                            if (finfo.GetParameters().Count() == 0 && finfo.Name == "Run")
                            {
                                RefClass r = new RefClass();
                                object obj = finfo.Invoke(r, null);
                            }
                        }
                    }
                }
            }
        }
    }
}