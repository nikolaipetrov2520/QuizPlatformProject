using Microsoft.EntityFrameworkCore;
using QuizPlatformProject.Data;
using QuizPlatformProject.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizPlatformProject.Services.Answares
{
    public class AnswaresService : IAnswaresService
    {
        private readonly ApplicationDbContext db;

        public AnswaresService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<PossibleAnswerOutputModel> GetAllAnswaresByQuestionId(int id)
        {
            var queriable = db.PossibleAnswers.Where(a => a.QuestionId == id).AsNoTracking();
            var answers = new List<PossibleAnswerOutputModel>();

            foreach (var item in queriable)
            {
                var answer = new PossibleAnswerOutputModel
                {
                    Id = item.Id,
                    Answer = item.Answare,
                };
                answers.Add(answer);
            }
            
            return answers;
        }

        public bool GetCorrectAnswerByQuestionId(int id, int answer)
        {
            var question = db.Questions.FirstOrDefault(x => x.Id == id);

            if (question.CorrectAnswer == answer)
            {
                return true;
            }

            return false;

        }
    }
}
