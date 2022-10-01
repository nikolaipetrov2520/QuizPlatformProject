using QuizPlatformProject.Data;
using QuizPlatformProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizPlatformProject.Services.Sessions
{
    public class SessionsService : ISessionsService
    {
        private readonly ApplicationDbContext db;

        public SessionsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void CloseSession(string key)
        {
            var session = db.Sessions.FirstOrDefault(s => s.Key == key);

            if (session != null)
            {
                session.IsClose = true;

                db.SaveChanges();
            }
        }

        public string CreateSession()
        {
            var sessionKey = Guid.NewGuid().ToString();
            var session = new Session
            {
                Key = sessionKey,
                IsClose = false,
            };

            db.Sessions.Add(session);
            db.SaveChanges();

            return sessionKey;
        }
    }
}
