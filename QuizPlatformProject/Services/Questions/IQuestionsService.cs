using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizPlatformProject.Services.Questions
{
    public interface IQuestionsService
    {
        List<string> GetAllCategories();
    }
}
