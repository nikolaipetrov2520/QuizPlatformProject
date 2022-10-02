namespace QuizPlatformProject.InputModels
{
    using System.Collections.Generic;

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
