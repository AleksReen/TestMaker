using System;
using System.Collections.Generic;
using System.Text;
using TestMaker.Data.Context;
using TestMaker.Models.ViewModels;

namespace TestMaker.Data.Processor.Providers
{
    public interface IQuestionProvider
    {
        IReadOnlyList<QuestionViewModel> GetQuestionViewModelsList(ApplicationDbContext context, int quizId);
        QuestionViewModel GetQuestion (ApplicationDbContext context, int id);
        QuestionViewModel PutQuestion(ApplicationDbContext context, QuestionViewModel m);
        QuestionViewModel PostQuestion(ApplicationDbContext context, QuestionViewModel m);
        ResultOperation DeleteQuestion(ApplicationDbContext context, int id);
    }
}
