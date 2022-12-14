namespace QuizPlatformProject.Services.Questions
{
    using Microsoft.EntityFrameworkCore;
    using QuizPlatformProject.Data;
    using System.Collections.Generic;

    public class CategoriesService : ICategoriesService
    {

        private readonly ApplicationDbContext db;

        public CategoriesService(ApplicationDbContext db)
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
