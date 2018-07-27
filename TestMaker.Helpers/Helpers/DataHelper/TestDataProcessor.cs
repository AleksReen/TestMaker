using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TestMaker.Models.ViewModels;

namespace TestMaker.Helpers.Helpers.DataHelper
{
    public class TestDataProcessor: ITestDataProcessor
    {
        public JsonSerializerSettings JsonSettings { get; set; } = new JsonSerializerSettings { Formatting = Formatting.Indented };

        public IReadOnlyList<QuizViewModel> GetQuizViewModelsList(int num)
        {

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

        public IReadOnlyList<QuestionViewModel> GetQuestionViewModelsList(int quizId)
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

        public IReadOnlyList<AnswerViewModel> GetAnswerViewModelsList(int questionId)
        {
            var sampleAnswers = new List<AnswerViewModel>()
            {
                new AnswerViewModel
                {
                    Id = 1,
                    QuestionId = questionId,
                    Text = "Friends and family",
                    CreateDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now
                }
            };

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

        public IReadOnlyList<ResultViewModel> GetResultViewModelsList(int quizId)
        {
            var sampleResults = new List<ResultViewModel>();

            sampleResults.Add(new ResultViewModel()
            {
                Id = 1,
                QuizId = quizId,
                Text = "What do you value most in your life?",
                CreateDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            });

            for (int i = 2; i <= 5; i++)
            {
                sampleResults.Add(new ResultViewModel()
                {
                    Id = i,
                    QuizId = quizId,
                    Text = $"Sample question № {i}",
                    CreateDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now
                });
            }

            return sampleResults;
        }

        public QuizViewModel GetQuizById(int id) => new QuizViewModel() {

            Id = id,
            Title = $"Sample quiz with id - {id}",
            Description = "Not a real quiz: it's just a sample!",
            CreateDate = DateTime.Now,
            LastModifiedDate = DateTime.Now

        };
    }
}
