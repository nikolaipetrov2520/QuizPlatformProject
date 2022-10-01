using Microsoft.AspNetCore.Mvc;
using QuizPlatformProject.Data;
using QuizPlatformProject.InputModels;
using QuizPlatformProject.OutputModels;
using QuizPlatformProject.Services.Answares;
using QuizPlatformProject.Services.Modes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizPlatformProject.Controllers
{
    [ApiController]
    [Route("/mode")]
    public class ModeController : ControllerBase
    {
        private readonly IModesService modesService;
        private readonly IAnswaresService answaresService;

        public ModeController(IModesService modesService, IAnswaresService answaresService)
        {
            this.modesService = modesService;
            this.answaresService = answaresService;
        }


        [HttpPost]
        public QuestionOutputModel Post(ModeInputModel input)
        {
            var question = new QuestionOutputModel();

            if (input.Mode.ToLower() == "normal")
            {
                question = modesService.GetQuestionsNormalMode();
                question.PossibleAnswer = answaresService.GetAllAnswaresByQuestionId(question.Id);
            }
            else if(input.Mode.ToLower() == "survival")
            {

            }

            return question;
        }
    }
}
