namespace QuizPlatformProject.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using QuizPlatformProject.InputModels;
    using QuizPlatformProject.OutputModels;
    using QuizPlatformProject.Services.Answares;
    using QuizPlatformProject.Services.Modes;
    using System.Collections.Generic;

    [ApiController]
    [Route("/questions")]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionsService questionsService;
        private readonly IAnswaresService answaresService;

        public QuestionController(IQuestionsService questionsService, IAnswaresService answaresService)
        {
            this.questionsService = questionsService;
            this.answaresService = answaresService;
        }


        [HttpPost]
        public List<QuestionOutputModel> Post(QuestionsInputModel input)
        {
            var questions = new List<QuestionOutputModel>();

            questions = questionsService.GetQuestions(input);

            foreach (var question in questions)
            {
                question.PossibleAnswer = answaresService.GetAllAnswaresByQuestionId(question.Id);
            }

            return questions;
        }
    }
}
