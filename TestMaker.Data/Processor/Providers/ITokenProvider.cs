using System;
using System.Collections.Generic;
using System.Text;
using TestMaker.Data.Context;
using TestMaker.Models.Data;
using TestMaker.Models.ViewModels;

namespace TestMaker.Data.Processor.Providers
{
    public interface ITokenProvider
    {
        Token GetToken(ApplicationDbContext context, TokenRequestViewModel model);
        void UpdateToken(ApplicationDbContext dbContext, Token oldToken, Token newToken);
        void AddNewRefreshToken(ApplicationDbContext dbContext, Token newToken);
    }
}
