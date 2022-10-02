namespace ClientApplication
{
    using ClientApplication.InputModels;
    using ClientApplication.OutputModels;
    using System;
    using System.Collections.Generic;

    class SurvivalGame
    {
        public static void Game(string baseUrl, string securityKey, List<string> categories)
        {
            Console.Clear();

            int count = 2;
            var result = new AnswerInputModel();
            bool wrongAnswer = false;

            var answers = new List<AnswerOutputModel>();

            for (int i = 0; i < 2; i++)
            {
                int difficulty = i;

                foreach (var selectedCategory in categories)
                {
                    var questions = Common.GetQuestions(baseUrl, securityKey, count, selectedCategory, difficulty);

                    foreach (var question in questions)
                    {
                        var answer = Common.OutputQuestionsGame(question);
                        answers.Add(answer);
                        result = Common.GetResult(baseUrl, securityKey, answers);
                        if (result.WrongCount > 0)
                        {
                            wrongAnswer = true;
                            break;
                        }
                    }
                    if (wrongAnswer)
                    {
                        break;
                    }
                }
                if (wrongAnswer)
                {
                    break;
                }
            }
            PrintResult(result);
        }

        private static void PrintResult(AnswerInputModel result)
        {
            int points = 0;

            for (int i = 1; i <= result.CorrectCount; i++)
            {
                if (i <= 6)
                {
                    points++;
                }
                else if (i <= 12)
                {
                    points += 2;
                }
                else
                {
                    points += 3;
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Common.Separator();
            Console.WriteLine("GAME OVER!!!");
            Common.Separator();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Correct answers - {result.CorrectCount}");
            Console.WriteLine($"Your score is - {points} points");
            Console.ForegroundColor = ConsoleColor.Red;
            Common.Separator();
            Console.ForegroundColor = ConsoleColor.White;

        }
    }
}
