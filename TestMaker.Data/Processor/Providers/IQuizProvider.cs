using System;
using System.Collections.Generic;
using System.Text;
using TestMaker.Data.Context;
using TestMaker.Models.ViewModels;

namespace TestMaker.Data.Processor.Providers
{
    public interface IQuizProvider
    {
        QuizViewModel GetQuiz(ApplicationDbContext context, int id);

        IReadOnlyList<QuizViewModel> GetQuizViewModelsList(ApplicationDbContext context, int num);

        IReadOnlyList<QuizViewModel> GetQuizByTitle(ApplicationDbContext context, int num);

        IReadOnlyList<QuizViewModel> GetQuizRandom(ApplicationDbContext context, int num);

        QuizViewModel PutQuiz (ApplicationDbContext context, QuizViewModel model);

        QuizViewModel PostQuiz(ApplicationDbContext context, QuizViewModel model);

        ResultOperation DeleteQuiz (ApplicationDbContext context, int model);
    }
}
