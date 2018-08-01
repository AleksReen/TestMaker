using Mapster;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestMaker.Data.Context;
using TestMaker.Data.Processor.Providers;
using TestMaker.Models.ViewModels;

namespace TestMaker.Data.Proccesor
{
    public class DataProcessor: IDataProcessor
    {

        public DataProcessor() { }

        #region Quiz

        public QuizViewModel GetQuiz(ApplicationDbContext context, int id) => context.Quizzes
                .Where(q => q.Id == id)
                .FirstOrDefault()
                .Adapt<QuizViewModel>();         

        public void PutQuiz(QuizViewModel q)
        {
            throw new NotImplementedException();
        }

        public void PostQuiz(QuizViewModel q)
        {
            throw new NotImplementedException();
        }

        public void DeleteQuiz(int id)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<QuizViewModel> GetQuizByTitle(ApplicationDbContext context, int num) => context.Quizzes
                .OrderBy(q => q.Title)
                .Take(num)
                .ToArray()
                .Adapt<QuizViewModel[]>();   

        public IReadOnlyList<QuizViewModel> GetQuizRandom(ApplicationDbContext context, int num) => context.Quizzes
                .OrderBy(q => Guid.NewGuid())
                .Take(num)
                .ToArray()
                .Adapt<QuizViewModel[]>();


        public IReadOnlyList<QuizViewModel> GetQuizViewModelsList(ApplicationDbContext context, int num) => context.Quizzes
                .OrderByDescending( q => q.CreatedDate)
                .Take(num)
                .ToArray()
                .Adapt<QuizViewModel[]>();

        #endregion

        #region Answer

        public IReadOnlyList<AnswerViewModel> GetAnswerViewModelsList(ApplicationDbContext context, int questionId)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Question
        public IReadOnlyList<QuestionViewModel> GetQuestionViewModelsList(ApplicationDbContext context, int quizId)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Result
        public IReadOnlyList<ResultViewModel> GetResultViewModelsList(ApplicationDbContext context, int quizId)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
