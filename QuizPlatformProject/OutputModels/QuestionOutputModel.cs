namespace QuizPlatformProject.OutputModels
{
    using System.Collections.Generic;

    public class QuestionOutputModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public IEnumerable<PossibleAnswerOutputModel> PossibleAnswer { get; set; }
    }
}
