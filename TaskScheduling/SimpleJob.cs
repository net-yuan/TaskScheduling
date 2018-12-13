using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskScheduling
{
    [PersistJobDataAfterExecution]
    [DisallowConcurrentExecution]
    public class SimpleJob : IJob
    {
        public const string Message = "msg";

        public virtual void Execute(IJobExecutionContext context)
        {
            try
            {
                JobKey jobKey = context.JobDetail.Key;
                string message = context.JobDetail.JobDataMap.GetString(Message);

                string strLog = String.Format("{0} 执行时间 {1}", jobKey, DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
            }
        }
    }
}