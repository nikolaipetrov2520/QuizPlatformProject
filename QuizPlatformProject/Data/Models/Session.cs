using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizPlatformProject.Data.Models
{
    public class Session
    {
        public int Id { get; set; }

        public string Key { get; set; }

        public bool IsClose { get; set; }
    }
}
