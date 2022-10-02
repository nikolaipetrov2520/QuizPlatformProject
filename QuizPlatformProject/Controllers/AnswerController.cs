using Microsoft.AspNetCore.Mvc;
using QuizPlatformProject.InputModels;
using QuizPlatformProject.OutputModels;
using QuizPlatformProject.Services.Answares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizPlatformProject.Controllers
{
    [ApiController]
    [Route("/answers")]
    public class AnswerController : Controller
    {
        private readonly IAnswaresService answaresService;

        public AnswerController(IAnswaresService answaresService)
        {
            this.answaresService = answaresService;
        }

        [HttpPost]
        public AnswerOutputModel Post(AnswerInputModel[] input)
        {
            int correctAnswerCount = 0;
            int wrongAnswerCount = 0;

            foreach (var item in input)
            {
                bool checking = answaresService.GetCorrectAnswerByQuestionId(item.QuestionId, item.Answer);
                if (checking)
                {
                    correctAnswerCount++;
                }
                else
                {
                    wrongAnswerCount++;
                }
            }

            var result = new AnswerOutputModel
            {
                CorrectCount = correctAnswerCount,
                WrongCount = wrongAnswerCount,
            };

            return result;
        }
    }
}
