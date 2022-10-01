using Microsoft.EntityFrameworkCore;
using QuizPlatformProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizPlatformProject.Services.Questions
{
    public class QuestionsService : IQuestionsService
    {

        private readonly ApplicationDbContext db;

        public QuestionsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<string> GetAllCategories()
        {
            var result = db.Categories.AsNoTracking();

            var categories = new List<string>();

            foreach (var categoriy in result)
            {
                categories.Add(categoriy.Name);
            }

            return categories;
        }
    }
}
