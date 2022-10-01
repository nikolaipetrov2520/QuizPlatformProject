using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizPlatformProject.OutputModels
{
    public class QuestionOutputModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int CorrectAnswer { get; set; }

        public IEnumerable<PossibleAnswerOutputModel> PossibleAnswer { get; set; }
    }
}
