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

        IReadOnlyList<QuizViewModel> GetQuizViewModelsList(int num);

        QuizViewModel GetQuizById(int id);

        IReadOnlyList<QuestionViewModel> GetQuestionViewModelsList(int quizId);

        IReadOnlyList<AnswerViewModel> GetAnswerViewModelsList(int questionId);

        IReadOnlyList<ResultViewModel> GetResultViewModelsList(int quizId);
    }
}
