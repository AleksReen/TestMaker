using System;
using System.Collections.Generic;
using System.Text;
using TestMaker.Data.Context;
using TestMaker.Models.ViewModels;

namespace TestMaker.Data.Processor.Providers
{
    public interface IAnswerProvider
    {
        IReadOnlyList<AnswerViewModel> GetAnswerViewModelsList(ApplicationDbContext context, int questionId);

        AnswerViewModel GetAnswer(ApplicationDbContext context, int id);

        AnswerViewModel PutAnswer(ApplicationDbContext context, AnswerViewModel m);

        AnswerViewModel PostAnswer(ApplicationDbContext context, AnswerViewModel m);

        ResultOperation DeleteAnswer(ApplicationDbContext context, int id);
    }
}
