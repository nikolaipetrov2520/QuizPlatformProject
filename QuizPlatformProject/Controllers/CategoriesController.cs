using Microsoft.AspNetCore.Mvc;
using QuizPlatformProject.Data;
using QuizPlatformProject.Services.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizPlatformProject.Controllers
{
    [ApiController]
    [Route("/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly IQuestionsService questionsService;

        public CategoriesController(IQuestionsService questionsService)
        {
            this.questionsService = questionsService;
        }

        [HttpGet]
        public List<string> Get()
        {
            var categories = questionsService.GetAllCategories();

            return categories;
        }

    }
}
