using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using TestMaker.Data.Context;
using TestMaker.Data.Processor.Providers;
using TestMaker.Models.Data;
using TestMaker.Models.ViewModels;

namespace TestMaker.Data.Proccesor
{
    public class DataProcessor: IDataProcessor
    {

        public DataProcessor() { }

        #region Quiz

        public QuizViewModel GetQuiz(ApplicationDbContext context, int id)
        {
            return context.Quizzes
                .Where(q => q.Id == id)
                .FirstOrDefault()
                .Adapt<QuizViewModel>();
        }

        public QuizViewModel PutQuiz(ApplicationDbContext context, QuizViewModel model)
        {
            var quiz = context.Quizzes.Where(q => q.Id == model.Id).FirstOrDefault();

            if (quiz == null) return null;

            quiz.Title = model.Title;
            quiz.Description = model.Description;
            quiz.Text = model.Text;
            quiz.Notes = model.Notes;
            quiz.UserId = model.UserId;

            quiz.LastModifiedDate = model.CreatedDate;

            context.SaveChanges();

            return quiz.Adapt<QuizViewModel>();
        }

        public QuizViewModel PostQuiz(ApplicationDbContext context, QuizViewModel model)
        {
            var quiz = new Quiz
            {
                Title = model.Title,
                Description = model.Description,
                Text = model.Text,
                Notes = model.Notes,

                CreatedDate = DateTime.Now,
                LastModifiedDate = model.CreatedDate,

                UserId = model.UserId
            };

            context.Add(quiz);
            context.SaveChanges();

            return quiz.Adapt<QuizViewModel>();
        }

        public ResultOperation DeleteQuiz(ApplicationDbContext context, int id)
        {
            var quiz = context.Quizzes.Where(q => q.Id == id).FirstOrDefault();

            if (quiz == null)
            {
                return ResultOperation.NotFound;
            }

            context.Quizzes.Remove(quiz);

            context.SaveChanges();

            return ResultOperation.Ok;
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
            return context.Answers
                .Where(i => i.QuestionId == questionId)
                .ToArray()
                .Adapt<IReadOnlyList<AnswerViewModel>>();
        }

        public AnswerViewModel GetAnswer(ApplicationDbContext context, int id)
        {
            return context.Answers
                .Where(i => i.Id == id)
                .FirstOrDefault()
                .Adapt<AnswerViewModel>();
        }

        public AnswerViewModel PutAnswer(ApplicationDbContext context, AnswerViewModel model)
        {
            var answer = context.Answers.Where(i => i.Id == model.Id).FirstOrDefault();

            if (answer == null) return null;

            answer.QuestionId = model.QuestionId;
            answer.Text = model.Text;
            answer.Value = model.Value;
            answer.Notes = model.Notes;

            answer.LastModifiedDate = model.CreatedDate;

            context.SaveChanges();

            return answer.Adapt<AnswerViewModel>();
        }

        public AnswerViewModel PostAnswer(ApplicationDbContext context, AnswerViewModel model)
        {
            var answer = model.Adapt<Answer>();

            answer.QuestionId = model.QuestionId;
            answer.Text = model.Text;
            answer.Notes = model.Notes;

            answer.CreatedDate = DateTime.Now;
            answer.LastModifiedDate = model.CreatedDate;

            context.Answers.Add(answer);
            context.SaveChanges();

            return answer.Adapt<AnswerViewModel>();
        }

        public ResultOperation DeleteAnswer(ApplicationDbContext context, int id)
        {
            var answer = context.Answers.Where(i => i.Id == id).FirstOrDefault();

            if (answer == null) return ResultOperation.NotFound;

            context.Answers.Remove(answer);

            context.SaveChanges();

            return ResultOperation.Ok;
        }

        #endregion

        #region Question
        public IReadOnlyList<QuestionViewModel> GetQuestionViewModelsList(ApplicationDbContext context, int quizId)
        {
            return context.Questions.Where(i => i.QuizId == quizId).ToArray().Adapt<IReadOnlyList<QuestionViewModel>>();
        }

        public QuestionViewModel GetQuestion(ApplicationDbContext context, int quizId)
        {
            return context.Questions.Where(i => i.Id == quizId)
                .FirstOrDefault()
                .Adapt<QuestionViewModel>();
        }

        public QuestionViewModel PutQuestion(ApplicationDbContext context, QuestionViewModel model)
        {
            var question = context.Questions.Where(i => i.Id == model.Id).FirstOrDefault();

            if (question == null) return null;

            question.QuizId = model.QuizId;
            question.Text = model.Text;
            question.Notes = model.Notes;

            question.LastModifiedDate = question.CreatedDate;

            context.SaveChanges();

            return question.Adapt<QuestionViewModel>();
        }

        public QuestionViewModel PostQuestion(ApplicationDbContext context, QuestionViewModel model)
        {
            var question = model.Adapt<Question>();

            question.QuizId = model.QuizId;
            question.Text = model.Text;
            question.Notes = model.Notes;

            question.CreatedDate = DateTime.Now;
            question.LastModifiedDate = model.CreatedDate;

            context.Questions.Add(question);
            context.SaveChanges();

            return question.Adapt<QuestionViewModel>();
        }

        public ResultOperation DeleteQuestion(ApplicationDbContext context, int id)
        {
            var question = context.Questions.Where(i => i.Id == id).FirstOrDefault();

            if (question == null) return ResultOperation.NotFound;

            context.Questions.Remove(question);

            context.SaveChanges();

            return ResultOperation.Ok;
        }
        #endregion

        #region Result
        public IReadOnlyList<ResultViewModel> GetResultViewModelsList(ApplicationDbContext context, int quizId)
        {          
            return context.Results
                .Where(i => i.QuizId == quizId)
                .ToArray()
                .Adapt<IReadOnlyList<ResultViewModel>>();
        }

        public ResultViewModel GetResult(ApplicationDbContext context, int id)
        {
            return context.Results.Where(i => i.Id == id)
                .FirstOrDefault()
                .Adapt<ResultViewModel>();
        }

        public ResultViewModel PutResult(ApplicationDbContext context, ResultViewModel model)
        {
            var result = context.Results.Where(i => i.Id == model.Id).FirstOrDefault();

            if (result == null) return null;

            result.QuizId = model.QuizId;
            result.Text = model.Text;
            result.MinValue = model.MinValue;
            result.MaxValue = model.MaxValue;
            result.Notes = model.Notes;

            result.LastModifiedDate = result.CreatedDate;

            context.SaveChanges();

            return result.Adapt<ResultViewModel>();
        }

        public ResultViewModel PostResult(ApplicationDbContext context, ResultViewModel model)
        {
            var result = model.Adapt<Result>();

            result.CreatedDate = DateTime.Now;
            result.LastModifiedDate = result.CreatedDate;

            context.Results.Add(result);
            context.SaveChanges();

            return result.Adapt<ResultViewModel>();
        }

        public ResultOperation DeleteResult(ApplicationDbContext context, int id)
        {
            var result = context.Results.Where(i => i.Id == id).FirstOrDefault();

            if (result == null) return ResultOperation.NotFound;

            context.Results.Remove(result);

            context.SaveChanges();

            return ResultOperation.Ok;
        }

        #endregion

        #region Token

        public Token GetToken(ApplicationDbContext context, TokenRequestViewModel model)
        {
            return context.Tokens.FirstOrDefault(t => t.ClientId == model.client_id && t.Value == model.refresh_token);
        }

        public void UpdateToken(ApplicationDbContext dbContext, Token oldToken, Token newToken)
        {
            dbContext.Tokens.Remove(oldToken);
            dbContext.Tokens.Add(newToken);

            dbContext.SaveChanges();
        }

        public void AddNewRefreshToken(ApplicationDbContext dbContext, Token newToken)
        {
            dbContext.Tokens.Add(newToken);

            dbContext.SaveChanges();
        }

        #endregion

        #region User

        public void Save(ApplicationDbContext context)
        {
            context.SaveChanges();
        }

        #endregion

    }
}
