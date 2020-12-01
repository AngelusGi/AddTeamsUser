using System.Configuration;
using System.Collections.Specialized;
using System.Collections.Generic;

namespace AuthProvider
{
    internal static class ConfigManager
    {
        internal static string GetClient()
        {

            //// Read a particular key from the config file 
            //string clientId = ConfigurationManager.AppSettings.Get("ClientId");
            //Console.WriteLine("The value of ClientId: " + clientId);
            //return clientId;

            return ConfigurationManager.AppSettings.Get("ClientId");
        }

        internal static string GetSecret()
        {
            return ConfigurationManager.AppSettings.Get("SecretId");
        }

        internal static string GetTenant()
        {
            return ConfigurationManager.AppSettings.Get("TenantId");

        }

        internal static List<string> GetAllData()
        {
            // Read all the keys from the config file
            NameValueCollection configAll;
            configAll = ConfigurationManager.AppSettings;

            var configValues = new List<string>();

            foreach (string value in configAll.AllKeys)
            {
                configValues.Add(configAll.Get(value));
            }

            return configValues;
        }
    }
}
