using TestMaker.Data.Context;

namespace TestMaker.Data.Processor.Providers
{
    public interface IUserProvider
    {
        void Save(ApplicationDbContext context);
    }
}
