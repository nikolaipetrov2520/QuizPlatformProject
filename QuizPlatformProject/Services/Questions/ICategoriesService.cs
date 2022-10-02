using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizPlatformProject.Services.Questions
{
    public interface ICategoriesService
    {
        List<string> GetAllCategories();
    }
}
