namespace ClientApplication.InputModels
{
    using System.Collections.Generic;

    public class QuestionInputModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public IEnumerable<PossibleAnswerInputModel> PossibleAnswer { get; set; }
    }
}
