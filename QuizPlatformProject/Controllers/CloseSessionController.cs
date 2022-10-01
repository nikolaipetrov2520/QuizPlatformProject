using Microsoft.AspNetCore.Mvc;
using QuizPlatformProject.Data;
using QuizPlatformProject.Services.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizPlatformProject.Controllers
{
    [ApiController]
    [Route("/close")]
    public class CloseSessionController : ControllerBase
    {
           
        private readonly ApplicationDbContext db;
        private readonly ISessionsService sessionsService;

        public CloseSessionController(ApplicationDbContext db, ISessionsService sessionsService)
        {
            this.db = db;
            this.sessionsService = sessionsService;
        }

        [HttpGet]
        public void Get()
        {
            var key = Request.Headers["Auth-Key"][0];

            sessionsService.CloseSession(key);
        }
    }
}
