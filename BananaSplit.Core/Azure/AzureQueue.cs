using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.StorageClient;

namespace BananaSplit.Core.Azure
{
    public class AzureQueue : AzureBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="messageText"></param>
        /// <param name="createQueue"></param>
        /// 
        public static void AddMessage(string queueName, string messageText, bool createQueue = true)
        {
            var cloudQueue = GetCloudQueue(queueName);
            var cloudQueueMessage = new CloudQueueMessage(messageText);
            if (createQueue)
            {
                cloudQueue.CreateIfNotExist();
            }
            cloudQueue.AddMessage(cloudQueueMessage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="cloudQueueMessage"></param>
        /// 
        public static void DeleteMessage(string queueName, CloudQueueMessage cloudQueueMessage)
        {
            if (cloudQueueMessage != null)
            {
                DeleteMessage(queueName, cloudQueueMessage.Id, cloudQueueMessage.PopReceipt);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="messageId"></param>
        /// <param name="popReceipt"></param>
        /// 
        public static void DeleteMessage(string queueName, string messageId, string popReceipt)
        {
            var cloudQueue = GetCloudQueue(queueName);
            cloudQueue.DeleteMessage(messageId, popReceipt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="createQueue"></param>
        /// <returns></returns>
        /// 
        public static bool IsQueueEmpty(string queueName, bool createQueue = true)
        {
            var cloudQueue = GetCloudQueue(queueName);
            if (createQueue)
            {
                cloudQueue.CreateIfNotExist();
            }
            if (null == cloudQueue.PeekMessage()) { return true; }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="timeoutInMinutes"></param>
        /// <param name="createQueue"></param>
        /// <returns></returns>
        /// 
        public static CloudQueueMessage GetMessage(string queueName, int? timeoutInMinutes, bool createQueue = true)
        {
            var cloudQueue = GetCloudQueue(queueName);
            if (createQueue)
            {
                cloudQueue.CreateIfNotExist();
            }
            return cloudQueue.GetMessage(new TimeSpan(0, 0, timeoutInMinutes ?? 10, 0));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="maxResults"></param>
        /// <param name="timeoutInMinutes"></param>
        /// <param name="createQueue"></param>
        /// <returns></returns>
        /// 
        public static IEnumerable<CloudQueueMessage> GetMessages(string queueName, int maxResults, int? timeoutInMinutes, bool createQueue = true)
        {
            var cloudQueue = GetCloudQueue(queueName);
            if (createQueue)
            {
                cloudQueue.CreateIfNotExist();
            }
            var time = new TimeSpan(0, 0, timeoutInMinutes ?? 10, 0);
            return cloudQueue.GetMessages(maxResults, time);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="maxResults"></param>
        /// <returns></returns>
        /// 
        public static IEnumerable<CloudQueueMessage> PeekMessages(string queueName, int maxResults = 32)
        {
            var cloudQueue = GetCloudQueue(queueName);
            var maxNum = (maxResults > 32) ? 32 : maxResults;
            return cloudQueue.PeekMessages(maxResults);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queueName"></param>
        /// <returns></returns>
        /// 
        private static CloudQueue GetCloudQueue(string queueName)
        {
            var cloudQueueClient = GetCloudStorageAccount().CreateCloudQueueClient();
            var cloudQueue = cloudQueueClient.GetQueueReference(queueName);
            return cloudQueue;
        }
    }
}
