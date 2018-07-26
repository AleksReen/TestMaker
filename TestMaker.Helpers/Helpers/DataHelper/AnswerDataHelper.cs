using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TestMaker.Models.ViewModels;

namespace TestMaker.Helpers.Helpers.DataHelper
{
    public static class AnswerDataHelper
    {
        public static JsonSerializerSettings JsonSettings { get; set; } = new JsonSerializerSettings { Formatting = Formatting.Indented };

        public static List<AnswerViewModel> GetAnswerViewModelsList(int questionId)
        {
            var sampleAnswers = new List<AnswerViewModel>() { new AnswerViewModel {

                Id = 1,
                QuestionId = questionId,
                Text = "Friends and family",
                CreateDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            } };

            for (int i = 2; i < 5; i++)
            {
                sampleAnswers.Add(new AnswerViewModel()
                {
                    Id = i,
                    QuestionId = questionId,
                    Text = $"Sample answer №{i}",
                    CreateDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now

                });
            }

            return sampleAnswers;
        }
    }
}
