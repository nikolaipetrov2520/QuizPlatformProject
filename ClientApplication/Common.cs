using ClientApplication.InputModels;
using ClientApplication.OutputModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace ClientApplication
{
    public class Common
    {
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
