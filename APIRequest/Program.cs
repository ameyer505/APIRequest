using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace APIRequest
{
    class Program
    {
        readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args) 
        {
            Program program = new Program();
            await program.GetUserInfo();
        }
        private async Task GetUserInfo() 
        {
            string response = await client.GetStringAsync("https://alexdmeyer.com/codetest/users.json");

            List<Objects> UserList = JsonConvert.DeserializeObject<List<Objects>>(response);

            //May need to create Temp folder if not already there
            using (StreamWriter file = File.CreateText(@"C:\Temp\results.csv"))
            {
                foreach (var item in UserList) 
            {
                    if (item.AccountEnabled == "true" && !item.DisplayName.Contains("S"))
                        {
                        
                        Console.WriteLine($"{item.UserPrincipalName} {item.DisplayName} {item.AccountEnabled}");

                        JsonSerializer serializer = new JsonSerializer();
                        
                        serializer.Serialize(file, $"Principal Name: {item.UserPrincipalName} Display Name: {item.DisplayName} Account Enabled: {item.AccountEnabled}");
                        }
                    }
            }
        }
        class Objects 
        {
            public string UserPrincipalName { get; set; }
            public string DisplayName { get; set; }
            public string AccountEnabled { get; set; }
        }
    }
}
