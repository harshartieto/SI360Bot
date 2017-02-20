using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SI.Biz.Core.Bot.Case.RepositoryLogics
{
    interface IFindMyOpenCases
    {
        IEnumerable<SI.Biz.Core.Bot.BotCase> GetMyOpenCases(string userId);
    }

    sealed class FindMyOpenCases : IFindMyOpenCases
    {
        public IEnumerable<BotCase> GetMyOpenCases(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
