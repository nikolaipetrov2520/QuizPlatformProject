namespace QuizPlatformProject.Data.Models
{
    using System.Collections.Generic;

    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Question> Questions { get; set; }
    }
}
