namespace QuizPlatformProject.Services.Questions
{
    using System.Collections.Generic;

    public interface ICategoriesService
    {
        List<string> GetAllCategories();
    }
}
