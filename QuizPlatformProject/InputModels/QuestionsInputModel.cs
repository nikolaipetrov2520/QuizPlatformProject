using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizPlatformProject.InputModels
{
    public class QuestionsInputModel
    {
        public int Count { get; set; }

        public string[] Categories { get; set; }

        public int Difficulty { get; set; }
    }
}
