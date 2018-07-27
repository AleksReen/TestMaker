using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TestMaker.Models.ViewModels;

namespace TestMaker.Helpers.Helpers.DataHelper
{
    public interface ITestDataProcessor
    {
        JsonSerializerSettings JsonSettings { get; set; }

        List<QuizViewModel> GetQuizViewModelsList(int num);

        List<QuestionViewModel> GetQuestionViewModelsList(int quizId);

        List<AnswerViewModel> GetAnswerViewModelsList(int questionId);
    }
}
