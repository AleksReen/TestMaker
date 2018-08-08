using System;
using System.Collections.Generic;
using System.Text;
using TestMaker.Data.Context;
using TestMaker.Models.ViewModels;

namespace TestMaker.Data.Processor.Providers
{
    public interface IResultProvider
    {
        IReadOnlyList<ResultViewModel> GetResultViewModelsList(ApplicationDbContext context, int quizId);

        ResultViewModel GetResult(ApplicationDbContext context, int id);

        ResultViewModel PutResult(ApplicationDbContext context, ResultViewModel model);

        ResultViewModel PostResult(ApplicationDbContext context, ResultViewModel model);

        ResultOperation DeleteResult(ApplicationDbContext context, int id);
    }
}
