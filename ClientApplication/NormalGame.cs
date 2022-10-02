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
            var questions = Common.GetQuestions(baseUrl, securityKey, count, selectedCategory, difficulty);
            Console.Clear();
            var answers = new List<AnswerOutputModel>();
            foreach (var question in questions)
            {
                var answer = Common.OutputQuestionsGame(question);

                answers.Add(answer);

            }           

            var result = Common.GetResult(baseUrl, securityKey, answers);

            PrintResult(result, difficulty);
        }

        private static void PrintResult(AnswerInputModel result, int difficulty)
        {
            int difficultyScore = difficulty + 1;
            Console.ForegroundColor = ConsoleColor.Red;
            Common.Separator();           
            Console.WriteLine("GAME OVER!!!");
            Common.Separator();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Correct answers - {result.CorrectCount}");
            Console.WriteLine($"Wrong answers - {result.WrongCount}");
            Console.WriteLine($"Your score is - {result.CorrectCount * difficultyScore} points"); 
            Console.ForegroundColor = ConsoleColor.Red;
            Common.Separator();
            Console.ForegroundColor = ConsoleColor.White;

        }
    }
}
