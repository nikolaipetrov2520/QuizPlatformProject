using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ClientApplication
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json", optional: false);

            IConfiguration config = builder.Build();

            var baseUrl = config.GetSection("baseUrl").Value;


            var serverStatus = ConnectToServer(baseUrl);

            if (serverStatus.Result == null)
            {
                Console.WriteLine("The Server is Offline");
                return;
            }

            var securiryKey = Newtonsoft.Json.JsonConvert.DeserializeObject(serverStatus.Result).ToString();

            Console.WriteLine("The server is Online");
            Console.WriteLine(new string('.', 60));

            var categories = GetCategories(baseUrl, securiryKey);

            if (categories == null)
            {
                return;
            }
            string choicenCategory = "";

            while (choicenCategory == "")
            {
                choicenCategory = ChoiseCategories(categories);
            }

            string choicenMode = "";

            while (choicenMode == "")
            {
                Console.WriteLine("Choice Game Mode (Normal or Survival)");
                string userMode = Console.ReadLine();

                if (userMode.ToLower() == "normal" || userMode.ToLower() == "survivel")
                {
                    choicenMode = userMode;
                }
            }

            if (choicenMode.ToLower() == "normal")
            {
                var difficulty = "";
                var questions = PostMode(baseUrl, securiryKey, choicenCategory, choicenMode, difficulty);
            }
            else
            {
                var questions = PostMode(baseUrl, securiryKey, choicenCategory, choicenMode);
            }

            




            CloseSession(baseUrl, securiryKey);


        }

        private static string ChoiseCategories(List<string> categories)
        {

            Console.WriteLine("Choice From awaible categories");
            Console.WriteLine(String.Join(" - ", categories));

            string choicenCategory = Console.ReadLine();

            var userCategories = choicenCategory.Split(",", StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in userCategories)
            {
                if (!categories.Contains(item))
                {
                    Console.WriteLine($"{item} is not valid Category");
                    return "";
                }
            }

            return choicenCategory;
        }

        private static async Task<string> ConnectToServer(string baseUrl)
        {
            string url = baseUrl + "/home";

            var response = await GetRequester(url);

            return response;
        }

        private static List<string> PostMode(
            string baseUrl,
            string securiryKey,
            string choicenCategory,
            string choicenMode,
            string difficulty = "")
        {
            string url = baseUrl + "/mode";

            var userCategories = choicenCategory.Split(",", StringSplitOptions.RemoveEmptyEntries);

            var data = new
            {
                categories = userCategories,
                mode = choicenMode,
                difficulty = difficulty,
            };

            var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            

            var responseText = new List<string>();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.Headers["Auth-Key"] = securiryKey;
            request.Timeout = 5000;

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(jsonData);
                streamWriter.Flush();
                streamWriter.Close();
            }
            var httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseString = streamReader.ReadToEnd();
                responseText = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(responseString);
                return responseText;
            }

            //try
            //{
            //    using (WebResponse response = (HttpWebResponse)request.GetResponse())
            //    {
            //        using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
            //        {
            //            var responseString = reader.ReadToEnd();
            //            responseText = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(responseString);
            //        }
            //    }
            //}
            //catch (WebException)
            //{
            //    responseText = null;
            //}

            return responseText;
        }

        private static List<string> GetCategories(string baseUrl, string securiryKey)
        {
            string url = baseUrl + "/categories";

            var responseText = new List<string>();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.Headers["Auth-Key"] = securiryKey;
            request.Timeout = 5000;

            try
            {
                using (WebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
                    {
                        var responseString = reader.ReadToEnd();
                        responseText = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(responseString);
                    }
                }
            }
            catch (WebException)
            {
                responseText = null;
            }

            return responseText;
        }

        private static void CloseSession(string baseUrl, string securiryKey)
        {
            string url = baseUrl + "/close";

            GetRequesterWithKey(url, securiryKey);

        }        

        private static async Task<string> GetRequester(string url)
        {

            string responseText = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.Timeout = 5000;

            try
            {
                using (WebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
                    {
                        responseText = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException)
            {
                responseText = null;
            }

            return responseText;

        }

        private static void GetRequesterWithKey(string url, string securiryKey)
        {
            string responseText = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.Headers["Auth-Key"] = securiryKey;
            request.Timeout = 5000;

            try
            {
                using (WebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
                    {
                        responseText = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException)
            {
                responseText = null;
            }

        }
    }
}
