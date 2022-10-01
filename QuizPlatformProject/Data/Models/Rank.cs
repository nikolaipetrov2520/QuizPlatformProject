using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizPlatformProject.Data.Models
{
    public class Rank
    {
        public int Id { get; set; }

        public int Num { get; set; }

        public IEnumerable<Question> Questions { get; set; }
    }
}
