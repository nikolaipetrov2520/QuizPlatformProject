namespace ClientApplication
{
    using ClientApplication.InputModels;
    using ClientApplication.OutputModels;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;

    public class Common
    {
        public static List<string> GetCategories(string baseUrl, string securityKey)
        {
            string url = baseUrl + "/";

            var response = GetRequester(url, securityKey);

            return response;
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

        public static string SelectCategories(List<string> categories)
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

        public static string SelectGamMode()
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

        public static List<QuestionInputModel> GetQuestions(string baseUrl, string securityKey, int count, string selectedCategory, int difficulty)
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

        public static int GetAnswerFromUserInput(string userInput)
        {
            int userAnswer = -1;

            switch (userInput.ToLower())
            {
                case "a":
                    userAnswer = 0;
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

            return userAnswer;
        }

        public static AnswerInputModel GetResult(string baseUrl, string securiryKey, List<AnswerOutputModel> answers)
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

        public static string PrintQuestion(QuestionInputModel question)
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

            return userInput;
        }

        public static AnswerOutputModel OutputQuestionsGame(QuestionInputModel question)
        {
            string userInput = PrintQuestion(question);
            int userAnswer = GetAnswerFromUserInput(userInput);


            var answer = new AnswerOutputModel
            {
                QuestionId = question.Id,
                Answer = userAnswer,
            };

            return answer;
        }

        public static void PrintCategories(List<string> categories)
        {
            Console.WriteLine("The server is Online");
            Common.Separator();
            Console.Write("Awaible categories is: ");
            Console.Write(String.Join(", ", categories));
            Console.WriteLine(" | Difficulties is: Easy, Medium, Hard");
            Common.Separator();
        }

        public static void Separator()
        {
            Console.WriteLine(new string('.', 110));
        }
    }
}
