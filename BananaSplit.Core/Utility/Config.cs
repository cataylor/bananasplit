using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace BananaSplit.Core.Utility
{
    public static class Config
    {
        public static String GetAppSetting(string key)
        {
            var setting = ConfigurationManager.AppSettings.Get(key);
            return setting;
        }

        public static String GetConnectionString(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }

        #region Encryption

        public static String EncryptPass
        {
            get { return "bsplit_pass"; }
        }

        public static String EncryptSalt
        {
            get { return "bsplit_salt"; }
        }

        #endregion

        #region Azure
        public static String AzureConnectionString
        {
            get { return GetAppSetting("AzureConnectionString"); }
        }

        public static String AzureCloudStoragePath
        {
            get { return GetAppSetting("AzureCloudStoragePath"); }
        }

        #endregion


        #region Facebook

        public static String FacebookId
        {
            get { return GetAppSetting("FacebookId"); }
        }

        public static String FacebookSecret
        {
            get { return GetAppSetting("FacebookSecret"); }
        }

        #endregion
    }
}
