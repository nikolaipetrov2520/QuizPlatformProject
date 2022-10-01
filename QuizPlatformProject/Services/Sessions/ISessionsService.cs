using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizPlatformProject.Services.Sessions
{
    public interface ISessionsService
    {
        string CreateSession();

        void CloseSession(string key);
    }
}
