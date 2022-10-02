using ClientApplication.InputModels;
using System;
using System.Collections.Generic;

namespace ClientApplication
{
    public class Common
    {
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
            Console.WriteLine(new string('.', 60));
        }
    }
}
