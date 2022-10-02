namespace QuizPlatformProject.Data
{
    using Microsoft.EntityFrameworkCore;
    using QuizPlatformProject.Data.Models;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Question> Questions { get; set; }

        public DbSet<PossibleAnswer> PossibleAnswers { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Rank> Ranks { get; set; }

    }
}
