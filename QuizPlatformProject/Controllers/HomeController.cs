namespace QuizPlatformProject.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using QuizPlatformProject.Services.Questions;
    using System.Collections.Generic;

    [ApiController]
    [Route("/")]
    public class HomeController : ControllerBase
    {
        private readonly ICategoriesService categoriesService;

        public HomeController(ICategoriesService questionsService)
        {
            this.categoriesService = questionsService;
        }

        [HttpGet]
        public List<string> Get()
        {
            var categories = categoriesService.GetAllCategories();

            return categories;
        }

    }
}
