using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SW.NetCore.Job.Model
{
    public class JobSchedule
    {
        /// <summary>
        /// Job識別名稱
        /// </summary>
        public string JobName { get; private set; }

        /// <summary>
        /// Job型別
        /// </summary>
        public Type JobType { get; private set; }

        /// <summary>
        /// Cron表示式
        /// </summary>
        public string CronExpression { get; private set; }

        public string JobDesc { get; private set; }
        /// <summary>
        /// Job狀態
        /// </summary>
        public JobStatus JobStatus { get; set; } = JobStatus.Init;

        public JobSchedule(Type jobType, string cronExpression, string jobName, string jobDesc)
        {
            JobType = jobType ?? throw new ArgumentNullException(nameof(jobType));
            CronExpression = cronExpression ?? throw new ArgumentNullException(nameof(cronExpression));
            JobName = jobName ?? throw new ArgumentNullException(nameof(jobName));
            JobDesc = jobDesc ?? throw new ArgumentNullException(nameof(jobDesc));

        }



    }


    public enum JobStatus : byte
    {
        [Description("初始化")]
        Init = 0,
        [Description("已排程")]
        Scheduled = 1,
        [Description("執行中")]
        Running = 2,
        [Description("已停止")]
        Stopped = 3,
    }
}
