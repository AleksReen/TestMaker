﻿using Mapster;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var quiz = new Quiz();
            quiz.Title = model.Title;
            quiz.Description = model.Description;
            quiz.Text = model.Text;
            quiz.Notes = model.Notes;

            quiz.CreatedDate = DateTime.Now;
            quiz.LastModifiedDate = model.CreatedDate;

            quiz.UserId = context.Users.Where(u => u.UserName == "Admin").FirstOrDefault().Id;

            context.Add(quiz);
            context.SaveChanges();

            return quiz.Adapt<QuizViewModel>();
        }

        public QuizViewModel PostQuiz(ApplicationDbContext context, QuizViewModel model)
        {
            var quiz = context.Quizzes.Where(q => q.Id == model.Id).FirstOrDefault();

            if (quiz == null) return null;        

            quiz.Title = model.Title;
            quiz.Description = model.Description;
            quiz.Text = model.Text;
            quiz.Notes = model.Notes;

            quiz.LastModifiedDate = model.CreatedDate;

            context.SaveChanges();

            return quiz.Adapt<QuizViewModel>();
        }

        public ResultOperation DeleteQuiz(ApplicationDbContext context, int id)
        {
            var quiz = context.Quizzes.Where(q => q.Id == id).FirstOrDefault();

            if (quiz == null)
            {
                return ResultOperation.CancelOperation;
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