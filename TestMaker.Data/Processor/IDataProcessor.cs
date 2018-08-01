using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TestMaker.Data.Context;
using TestMaker.Data.Processor.Providers;
using TestMaker.Models.ViewModels;

namespace TestMaker.Data.Proccesor
{
    public interface IDataProcessor: IQuizProvider, IAnswerProvider, IQuestionProvider, IResultProvider
    {}
}
