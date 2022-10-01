using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizPlatformProject.Data;
using QuizPlatformProject.Data.Models;
using QuizPlatformProject.Services.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizPlatformProject.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string APIKEY = "Auth-Key";

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, ApplicationDbContext db)
        {
            if (context.Request.Path != "/home")
            {
                if (!context.Request.Headers.TryGetValue(APIKEY, out
                                    var extractedApiKey))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Api Key was not provided ");
                    return;
                }

                var apiKey = db.Sessions.Where(x => !x.IsClose).FirstOrDefault(a => a.Key == extractedApiKey.ToString());

                if (apiKey == null)
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Unauthorized client");
                    return;
                }
            }
           
            await _next(context);
        }
    }
}
