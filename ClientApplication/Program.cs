using ClientApplication.InputModels;
using ClientApplication.OutputModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
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

            var securityKey = config.GetSection("securityKey").Value;

            var categories = GetCategories(baseUrl, securityKey);

            if (categories == null)
            {
                Console.WriteLine("The Server is Offline");
                return;
            }

            Console.WriteLine("The server is Online");
            Separator();
            Console.Write("Awaible categories is: ");
            Console.Write(String.Join(", ", categories));
            Console.WriteLine(" | Difficulties is: Easy, Medium, Hard");
            Separator();

            string selectedCategory = "";

            while (selectedCategory == "")
            {
                selectedCategory = SelectCategories(categories);
            }

            Separator();
            string selectedMode = "";

            while (selectedMode == "")
            {

                Console.WriteLine("Select Game Mode (Normal or Survival):");
                string userMode = Console.ReadLine();

                if (userMode.ToLower() == "normal" || userMode.ToLower() == "survivel")
                {
                    selectedMode = userMode;
                }
                else
                {
                    Console.WriteLine($"{userMode} is not valid Mode");
                }
            }

            if (selectedMode.ToLower() == "normal")
            {
                NormalGame(baseUrl, securityKey, selectedCategory);
            }
            else
            {
                int count = 3;
                var questions = GetQuestions(baseUrl, securityKey, count, selectedCategory);
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

            Console.WriteLine("Select categories:");

            string selectedCategory = Console.ReadLine();

            var userCategories = selectedCategory.Split(",", StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in userCategories)
            {
                if (!categories.Contains(item))
                {
                    Console.WriteLine($"{item} is not valid Category");
                    return "";
                }
            }

            return selectedCategory;
        }

        private static void Separator()
        {
            Console.WriteLine(new string('.', 60));
        }

        private static void NormalGame(string baseUrl,string securityKey, string selectedCategory)
        {
            Separator();
            int difficulty = -1;

            while (difficulty == -1)
            {

                Console.WriteLine("Select Difficulty:");
                string userDifficulty = Console.ReadLine();

                if (userDifficulty.ToLower() == "easy")
                {
                    difficulty = 0;
                }
                else if (userDifficulty.ToLower() == "medium")
                {
                    difficulty = 1;
                }
                else if (userDifficulty.ToLower() == "hard")
                {
                    difficulty = 2;
                }
                else
                {
                    Console.WriteLine($"{userDifficulty} is not valid Difficulty");
                }
            }
            int count = 10;
            var questions = GetQuestions(baseUrl, securityKey, count, selectedCategory, difficulty);

            var answers = PrintQuestions(questions);

            var result = GetResult(baseUrl, securityKey, answers);

            PrintResult(result, difficulty);
        }

        private static List<AnswerOutputModel> PrintQuestions(List<QuestionInputModel> questions)
        {
            var answers = new List<AnswerOutputModel>();
            Console.Clear();
            foreach (var question in questions)
            {
                Separator();
                Console.WriteLine(question.Text);
                int charCode = 97;
                foreach (var posibleAnswer in question.PossibleAnswer)
                {
                    Console.Write((char)charCode + " - ");
                    Console.WriteLine(posibleAnswer.Answer);
                    charCode++;
                }
                string userInput = Console.ReadLine();

                int userAnswer = -1;

                switch (userInput.ToLower())
                {
                    case "a": userAnswer = 0;
                            break;
                    case "b":
                        userAnswer = 1;
                        break;
                    case "c":
                        userAnswer = 2;
                        break;
                    case "d":
                        userAnswer = 3;
                        break;
                    default:
                        break;
                }

                var answer = new AnswerOutputModel
                {
                    QuestionId = question.Id,
                    Answer = userAnswer,
                };

                answers.Add(answer);

            }
            return answers;
        }

        private static AnswerInputModel GetResult(string baseUrl, string securiryKey, List<AnswerOutputModel> answers)
        {
            string url = baseUrl + "/answers";

            var jsonData = JsonConvert.SerializeObject(answers);


            var result = new AnswerInputModel();

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
                result = JsonConvert.DeserializeObject<AnswerInputModel>(responseString);
                return result;
            }

        }

        private static void PrintResult(AnswerInputModel result, int difficulty)
        {
            int difficultyScore = difficulty + 1;
            Separator();

            Console.WriteLine($"Correct answers - {result.CorrectCount}");
            Console.WriteLine($"Wrong answers - {result.WrongCount}");
            Console.WriteLine($"You have - {result.CorrectCount * difficultyScore} scores");

            Separator();

        }

        private static List<QuestionInputModel> GetQuestions(
            string baseUrl,
            string securityKey,
            int count,
            string selectedCategory,
            int difficulty = -1)
        {
            string url = baseUrl + "/questions";

            var userCategories = selectedCategory.Split(",", StringSplitOptions.RemoveEmptyEntries);

            var data = new
            {
                count = count,
                categories = userCategories,
                difficulty = difficulty,
            };

            var jsonData = JsonConvert.SerializeObject(data);
            

            var questions = new List<QuestionInputModel>();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.Headers["Auth-Key"] = securityKey;
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
                questions = JsonConvert.DeserializeObject<List<QuestionInputModel>>(responseString);
                return questions;
            }

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
