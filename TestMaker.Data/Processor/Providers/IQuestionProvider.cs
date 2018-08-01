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
    }
}
