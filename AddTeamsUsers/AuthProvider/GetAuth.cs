﻿using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;

using System;
using System.Configuration;
using System.Threading.Tasks;



namespace AuthProvider
{
    public static class GetAuth { 
    

        public static string Secret1 { get; set; }
        public static string Secret2 { get; set; }

        public static string GetMySecret()
    {
        var moviesConfig = Configuration.GetSection("Movies").Get<MovieSettings>();
        return moviesConfig.ServiceApiKey;
    }


        public static void GetTest()
        {
            ConfigManager.GetAllData();
        }

        public static void DoAuth()
        {
            try
            {
                GetUsersAsync().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private async static Task GetUsersAsync()
        {
            
            IConfidentialClientApplication confidentialClientApplication = ConfidentialClientApplicationBuilder
                .Create(ConfigManager.GetClient())
                .WithTenantId(ConfigManager.GetTenant())
                .WithClientSecret(ConfigManager.GetSecret())
                .Build();

            ClientCredentialProvider authProvider = new ClientCredentialProvider(confidentialClientApplication);
            GraphServiceClient graphClient = new GraphServiceClient(authProvider);

            var groups = await graphClient.Groups.Request().Select(group => new { group.Id, group.DisplayName }).GetAsync();

            Console.WriteLine("Group name \t|\tGroup Id");

            foreach (var group in groups)
            {
                Console.WriteLine($"{group.DisplayName}, {group.Id}");
            }
        }
    }
}
