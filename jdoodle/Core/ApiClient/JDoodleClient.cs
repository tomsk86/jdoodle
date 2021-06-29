using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net.Http;

namespace jdoodle.Core.ApiClient
{
    public class JDoodleClient : HttpClient
    {
        public string ClientSecret { get; set; }
        public string ClientID { get; set; }

        public JDoodleClient()
        {
            BaseAddress = new Uri(ConfigurationManager.AppSetting["JDoodle:BaseUrl"]);
            ClientID = ConfigurationManager.AppSetting["JDoodle:ClientID"];
            ClientSecret = ConfigurationManager.AppSetting["JDoodle:ClientSecret"];
        }

        private static class ConfigurationManager
        {
            public static IConfiguration AppSetting { get; }

            static ConfigurationManager()
            {
                AppSetting = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("apiSettingFile.json")
                        .Build();
            }
        }
    }
}