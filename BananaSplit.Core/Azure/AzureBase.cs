using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BananaSplit.Core.Utility;
using Microsoft.WindowsAzure;

namespace BananaSplit.Core.Azure
{
    public class AzureBase
    {
        protected static CloudStorageAccount GetCloudStorageAccount()
        {
            // Set Configuration File...
            CloudStorageAccount.SetConfigurationSettingPublisher((configName, configSetter) => configSetter(Config.AzureConnectionString));
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.FromConfigurationSetting(String.Empty);
            return cloudStorageAccount;
        }
    }
}
