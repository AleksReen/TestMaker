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
    }
}
