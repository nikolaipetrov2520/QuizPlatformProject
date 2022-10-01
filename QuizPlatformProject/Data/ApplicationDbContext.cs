using Microsoft.EntityFrameworkCore;
using QuizPlatformProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizPlatformProject.Data
{
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
