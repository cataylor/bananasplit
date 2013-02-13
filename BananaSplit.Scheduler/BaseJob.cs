using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Quartz;
using log4net;

namespace BananaSplit.Scheduler
{
    public abstract class BaseJob : IInterruptableJob
    {
        protected ILog Log { get; private set; }

        protected Boolean isJobInterrupted { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// 
        protected BaseJob()
        {
            this.isJobInterrupted = false;
            log4net.Config.XmlConfigurator.Configure();
            this.Log = LogManager.GetLogger(this.GetType());
        }

        /// <summary>
        /// Method must be overridden in child class
        /// </summary>
        /// <param name="context"></param>
        /// 
        public abstract void RunJob(IJobExecutionContext context);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// 
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                RunJob(context);
            }
            catch (Exception jobEx)
            {
                Log.ErrorFormat("JobName {0} Failed with Error:{1}. The error details are:{2}", this.GetType().Name, jobEx.Message, jobEx.StackTrace);
            }
        }

        /// <summary>
        /// If this method is called the job's been interrupted by Quartz. This allows the dev to take appropriate action in response to a stop
        /// </summary>
        /// 
        public void Interrupt()
        {
            this.isJobInterrupted = true;
        }
    }
}

