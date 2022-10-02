using QuizPlatformProject.Data;
using QuizPlatformProject.InputModels;
using QuizPlatformProject.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizPlatformProject.Services.Modes
{
    public interface IQuestionsService
    {
        List<QuestionOutputModel> GetQuestionsMode(QuestionsInputModel input);
    }
}
