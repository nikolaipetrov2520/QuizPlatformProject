using QuizPlatformProject.Data;
using QuizPlatformProject.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizPlatformProject.Services.Modes
{
    public class ModesService : IModesService
    {
        private readonly ApplicationDbContext db;

        public ModesService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public QuestionOutputModel GetQuestionsNormalMode()
        {
            var queriable = db.Questions.FirstOrDefault();

            var questions = new QuestionOutputModel
            {
                Id = queriable.Id,
                Text = queriable.Text,
                CorrectAnswer = queriable.CorrectAnswer
            };

            return questions;
        }
    }
}
