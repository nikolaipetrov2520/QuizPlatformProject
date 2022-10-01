using QuizPlatformProject.Data;
using QuizPlatformProject.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizPlatformProject.Services.Modes
{
    public interface IModesService
    {
        QuestionOutputModel GetQuestionsNormalMode();
    }
}
