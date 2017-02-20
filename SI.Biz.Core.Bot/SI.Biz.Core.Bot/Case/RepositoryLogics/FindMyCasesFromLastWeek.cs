using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SI.Biz.Core.Bot.Case.RepositoryLogics
{
    interface IFindCasesFromLastWeek
    {
        IEnumerable<SI.Biz.Core.Bot.BotCase> GetMyCasesFromLastWeek(string userId);
    }

    sealed class FindMyCasesFromLastWeek : IFindCasesFromLastWeek
    {
        public IEnumerable<BotCase> GetMyCasesFromLastWeek(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
