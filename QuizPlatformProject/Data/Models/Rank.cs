namespace QuizPlatformProject.Data.Models
{
    using System.Collections.Generic;

    public class Rank
    {
        public int Id { get; set; }

        public int Num { get; set; }

        public IEnumerable<Question> Questions { get; set; }
    }
}
