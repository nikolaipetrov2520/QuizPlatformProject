namespace ClientApplication
{
    using Microsoft.Extensions.Configuration;
    using System;
    using System.IO;

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

            var categories = Common.GetCategories(baseUrl, securityKey);

            if (categories == null)
            {
                Console.WriteLine("The Server is Offline");
                return;
            }
            Common.PrintCategories(categories);
           
            string selectedCategory = Common.SelectCategories(categories);

            string selectedMode = Common.SelectGamMode();

            if (selectedMode.ToLower() == "normal")
            {
                NormalGame.Game(baseUrl, securityKey, selectedCategory);
            }
            else
            {
                SurvivalGame.Game(baseUrl, securityKey, categories);
            }
        }
    }
}
