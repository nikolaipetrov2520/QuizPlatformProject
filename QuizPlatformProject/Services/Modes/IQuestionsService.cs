namespace QuizPlatformProject.Services.Modes
{
    using QuizPlatformProject.InputModels;
    using QuizPlatformProject.OutputModels;
    using System.Collections.Generic;
    public interface IQuestionsService
    {
        List<QuestionOutputModel> GetQuestions(QuestionsInputModel input);
    }
}
