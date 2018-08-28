using TestMaker.Data.Processor.Providers;

namespace TestMaker.Data.Proccesor
{
    public interface IDataProcessor: IQuizProvider, IAnswerProvider, IQuestionProvider, IResultProvider, ITokenProvider,IUserProvider
    {}
}
