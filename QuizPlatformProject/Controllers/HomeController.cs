using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using QuizPlatformProject.Data;
using QuizPlatformProject.Data.Models;
using QuizPlatformProject.Services.Questions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace QuizPlatformProject.Controllers
{
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
