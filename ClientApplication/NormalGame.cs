using ClientApplication.InputModels;
using ClientApplication.OutputModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;


namespace ClientApplication
{
    class NormalGame
    {
        public static void Game(string baseUrl, string securityKey, string selectedCategory)
        {
            Common.Separator();
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

            var answers = OutputQuestionsInNormalGame(questions);

            var result = GetResult(baseUrl, securityKey, answers);

            PrintResult(result, difficulty);
        }

        private static List<QuestionInputModel> GetQuestions(string baseUrl, string securityKey, int count, string selectedCategory, int difficulty = -1)
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

        private static List<AnswerOutputModel> OutputQuestionsInNormalGame(List<QuestionInputModel> questions)
        {
            var answers = new List<AnswerOutputModel>();
            Console.Clear();
            foreach (var question in questions)
            {
                string userInput = Common.PrintQuestion(question);
                int userAnswer = Common.GetAnswerFromUserInput(userInput);


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
            Common.Separator();

            Console.WriteLine($"Correct answers - {result.CorrectCount}");
            Console.WriteLine($"Wrong answers - {result.WrongCount}");
            Console.WriteLine($"Your score is - {result.CorrectCount * difficultyScore} points");

            Common.Separator();

        }
    }
}
