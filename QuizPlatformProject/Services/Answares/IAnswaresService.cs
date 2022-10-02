namespace QuizPlatformProject.Services.Answares
{
    using QuizPlatformProject.OutputModels;
    using System.Collections.Generic;

    public interface IAnswaresService
    {
        List<PossibleAnswerOutputModel> GetAllAnswaresByQuestionId(int Id);

        bool GetCorrectAnswerByQuestionId(int id, int answer);
    }
}
