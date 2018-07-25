using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMaker.Models.ViewModels;

namespace TestMakerWebApp.Helpers.DataHelper
{
    public static class QuizDataHelper
    {
        public static JsonSerializerSettings JsonSettings { get; set; } = new JsonSerializerSettings { Formatting = Formatting.Indented };

        public static List<QuizViewModel> GetQuizViewModelsList(int num) {

            var sampleQuizzes = new List<QuizViewModel>() {
                new QuizViewModel {
                    Id = 1,
                    Title =  "Which Shingeki No Kyojin character are you?",
                    Description =  "Anime-related personality test",
                    CreateDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now
                }
            };

            for (int i = 2; i <= num; i++)
            {
                sampleQuizzes.Add(new QuizViewModel()
                {
                    Id = i,
                    Title = $"Sample Quiz {i}",
                    Description = "This is a sample quiz",
                    CreateDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now
                });
            }

            return sampleQuizzes;
        }
    }
}
