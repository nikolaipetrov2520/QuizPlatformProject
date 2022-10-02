namespace QuizPlatformProject.Data.Seeding
{
    using Newtonsoft.Json;
    using QuizPlatformProject.Data.Models;
    using QuizPlatformProject.InputModels;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class QuestionsSeeder : ISeeder
    {
        public Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.Questions.Any())
            {
                using (StreamReader r = new StreamReader("questions.json"))
                {
                    string json = r.ReadToEnd();
                    var inputQuestions = JsonConvert.DeserializeObject<List<InputQuestion>>(json);

                    foreach (var inputQuestion in inputQuestions)
                    {
                        var category = dbContext.Categories.FirstOrDefault(c => c.Name == inputQuestion.Category);

                        if (category == null)
                        {
                            category = new Category 
                            {
                                Name = inputQuestion.Category
                            };

                            dbContext.Categories.Add(category);

                            dbContext.SaveChanges();
                        }

                        var rank = dbContext.Ranks.FirstOrDefault(c => c.Num == inputQuestion.Rank);

                        if (rank == null)
                        {
                            rank = new Rank
                            {
                                Num = inputQuestion.Rank
                            };

                            dbContext.Ranks.Add(rank);

                            dbContext.SaveChanges();
                        }

                        var question = new Question
                        { 
                            RankId = rank.Id,
                            CategoryId = category.Id,
                            Text = inputQuestion.Text,
                            CorrectAnswer = inputQuestion.CorrectAnswer,
                        };

                        dbContext.Questions.Add(question);

                        dbContext.SaveChanges();

                        foreach (var item in inputQuestion.PossibleAnswers)
                        {
                            var answare = new PossibleAnswer 
                            {
                                QuestionId = question.Id,
                                Answare = item,
                            };

                            dbContext.PossibleAnswers.Add(answare);

                            dbContext.SaveChanges();
                        }
                    }
                }               
            }
            return Task.CompletedTask;
        }
    }
}
