using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizPlatformProject.Data
{
    public class PossibleAnswer
    {
        public int Id { get; set; }

        public string Answare { get; set; }

        public int QuestionId { get; set; }

        public Question Question { get; set; }
    }
}
