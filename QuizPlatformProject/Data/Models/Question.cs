using QuizPlatformProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizPlatformProject.Data
{
    public class Question
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int RankId { get; set; }

        public Rank Rank { get; set; }

        public string Text { get; set; }

        public ICollection<PossibleAnswer> PossibleAnswers { get; set; } = new HashSet<PossibleAnswer>();

        public int CorrectAnswer { get; set; }
    }
}
