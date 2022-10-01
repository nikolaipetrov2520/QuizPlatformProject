using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using QuizPlatformProject.Data;
using QuizPlatformProject.Data.Models;
using QuizPlatformProject.Services.Sessions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace QuizPlatformProject.Controllers
{
    [ApiController]
    [Route("/home")]
    public class HomeController : ControllerBase
    {
        private readonly ISessionsService sessionsService;

        public HomeController(ISessionsService sessionsService)
        {
            this.sessionsService = sessionsService;
        }

        [HttpGet]
        public string Get()
        {

            var key = sessionsService.CreateSession();


            return key;
        }

    }
}
