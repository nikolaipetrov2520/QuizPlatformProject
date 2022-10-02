using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;


namespace ClientApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json", optional: false);

            IConfiguration config = builder.Build();

            var baseUrl = config.GetSection("baseUrl").Value;

            var securityKey = config.GetSection("securityKey").Value;

            var categories = GetCategories(baseUrl, securityKey);

            if (categories == null)
            {
                Console.WriteLine("The Server is Offline");
                return;
            }
            Common.PrintCategories(categories);
           
            string selectedCategory = SelectCategories(categories);

            string selectedMode = SelectGamMode();

            if (selectedMode.ToLower() == "normal")
            {
                NormalGame.Game(baseUrl, securityKey, selectedCategory);
            }
            else
            {
                SurvivalGame.Game(baseUrl, securityKey, categories);
            }


        }

        private static List<string> GetCategories(string baseUrl, string securityKey)
        {
            string url = baseUrl + "/";

            var response = GetRequester(url, securityKey);

            return response;
        }

        private static string SelectCategories(List<string> categories)
        {
            string selectedCategory = "";

            while (selectedCategory == "")
            {
                Console.WriteLine("Select categories:");

                string inputCategory = Console.ReadLine();

                var userCategories = inputCategory.Split(",", StringSplitOptions.RemoveEmptyEntries);

                foreach (var item in userCategories)
                {
                    if (!categories.Contains(item))
                    {
                        Console.WriteLine($"{item} is not valid Category");
                        selectedCategory = "";
                        break;
                    }
                    selectedCategory = inputCategory;
                }
                
            }          
            return selectedCategory;
        }

        private static string SelectGamMode()
        {
            Common.Separator();
            string selectedMode = "";

            while (selectedMode == "")
            {

                Console.WriteLine("Select Game Mode (Normal or Survival):");
                string userMode = Console.ReadLine();

                if (userMode.ToLower() == "normal" || userMode.ToLower() == "survival")
                {
                    selectedMode = userMode;
                }
                else
                {
                    Console.WriteLine($"{userMode} is not valid Mode");
                }
            }
            return selectedMode;
        }

        private static List<string> GetRequester(string url, string securityKey)
        {

            var responseText = new List<string>();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.Timeout = 5000;
            request.Headers["Auth-Key"] = securityKey;

            try
            {
                using (WebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
                    {
                        var responseString = reader.ReadToEnd();
                        responseText = JsonConvert.DeserializeObject<List<string>>(responseString);
                    }
                }
            }
            catch (WebException)
            {
                responseText = null;
            }

            return responseText;

        }

    }
}
