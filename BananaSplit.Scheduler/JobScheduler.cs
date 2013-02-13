using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Quartz;
using Quartz.Impl;
using log4net;

namespace BananaSplit.Scheduler
{
    /// <summary>
    /// This singleton class starts the scheduler running
    /// </summary>
    /// 
    public static class JobScheduler
    {
        private static IScheduler scheduler;
        private static readonly ILog Log;

        static JobScheduler()
        {
            log4net.Config.XmlConfigurator.Configure();
            Log = LogManager.GetLogger(typeof(JobScheduler));
        }

        /// <summary>
        /// Starts scheduling the jobs to be run
        /// </summary>
        /// 
        public static void Start()
        {
            try
            {
                Log.Info("Starting Bananasplit.Scheduler");
                WriteEventLog("Job Scheduler is setting up Quartz");
                var properties = GetProperties();
                var schedulerFactory = new StdSchedulerFactory(properties);
                scheduler = schedulerFactory.GetScheduler();
                scheduler.Start();
            }
            catch (Exception e)
            {
                WriteEventLog("Failed with Exception: " + e.Message);
            }
        }


        private static NameValueCollection GetProperties()
        {
            var properties = new NameValueCollection();
            properties["quartz.scheduler.instanceName"] = "JobScheduler";
            properties["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz";
            properties["quartz.threadPool.threadCount"] = "15";
            properties["quartz.threadPool.threadPriority"] = "Normal";
            properties["quartz.jobStore.misfireThreshold"] = "60000";
            properties["jobStore.type"] = "Quartz.Simpl.RAMJobStore, Quartz";
            properties["quartz.plugin.xml.type"] = "Quartz.Plugin.Xml.JobInitializationPlugin, Quartz";
            properties["quartz.plugin.xml.fileNames"] = "~/quartz_jobs.xml";

            WriteEventLog("Properties written for Quartz");

            return properties;
        }

        /// <summary>
        /// Stops all jobs, but waits for any running jobs to complete before shutting down
        /// </summary>
        /// 
        public static void Stop()
        {
            Log.Info("Stopping BananaSplit.Scheduler");
            if (null != scheduler)
            {
                WriteEventLog("Shutting down Quartz");
                scheduler.Shutdown(true);
            }
        }

        private static void WriteEventLog(string message)
        {
            EventLog.WriteEntry("BananaSplitScheduler", message, EventLogEntryType.Information);
        }

    }
}
