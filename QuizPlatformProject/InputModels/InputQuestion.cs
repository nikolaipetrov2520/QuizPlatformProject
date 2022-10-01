using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizPlatformProject.InputModels
{
    public class InputQuestion
    {
        public int Id { get; set; }

        public string Category { get; set; }

        public int Rank { get; set; }

        public string Text { get; set; }

        public List<string> PossibleAnswers { get; set; }

        public int CorrectAnswer { get; set; }
    }
}
