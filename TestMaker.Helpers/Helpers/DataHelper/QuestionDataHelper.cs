using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TestMaker.Models.ViewModels;

namespace TestMaker.Helpers.Helpers.DataHelper
{
    public static class QuestionDataHelper
    {
        public static JsonSerializerSettings JsonSettings { get; set; } = new JsonSerializerSettings { Formatting = Formatting.Indented };

        public static List<QuestionViewModel> GetQuestionViewModelsList(int quizId)
        {
            var sampleQuestions = new List<QuestionViewModel>()
            {
                new QuestionViewModel () {
                    Id = 1,
                    QuizId = quizId,
                    Text = "What do you value most in your life?",
                    CreateDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now
                }
            };

            for (int i = 2; i <= 5; i++)
            {
                sampleQuestions.Add(new QuestionViewModel()
                {
                    Id = i,
                    QuizId = quizId,
                    Text = $"Sample question № {i}",
                    CreateDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now
                });
            }


            return sampleQuestions;
        }
    }
}
