using QuizPlatformProject.Data;
using QuizPlatformProject.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizPlatformProject.Services.Answares
{
    public interface IAnswaresService
    {
        List<PossibleAnswerOutputModel> GetAllAnswaresByQuestionId(int Id);
    }
}
