using Microsoft.EntityFrameworkCore;
using QuizPlatformProject.Data;
using QuizPlatformProject.InputModels;
using QuizPlatformProject.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizPlatformProject.Services.Modes
{
    public class QuestionsService : IQuestionsService
    {
        private readonly ApplicationDbContext db;

        public QuestionsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<QuestionOutputModel> GetQuestions(QuestionsInputModel input)
        {
            var queriable = db.Questions.Where(d => d.Rank.Num == input.Difficulty).Include(x => x.Category).ToList();

            var allQuestions = new List<Question>();
            foreach (var category in input.Categories)
            {
                var question = queriable.Where(x => x.Category.Name == category).ToList();

                allQuestions.AddRange(question);
            }
          

            var questions = new List<QuestionOutputModel>();

            foreach (var item in allQuestions)
            {
                var question = new QuestionOutputModel
                {
                    Id = item.Id,
                    Text = item.Text,
                };
                questions.Add(question);
            }

            Random rand = new Random();
            var shuffled = questions.OrderBy(x => rand.Next()).Take(input.Count).ToList();


            return shuffled;
        }
    }
}
