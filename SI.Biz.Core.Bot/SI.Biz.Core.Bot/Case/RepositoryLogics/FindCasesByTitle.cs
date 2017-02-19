using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SI.Biz.Core.Bot.Case.RepositoryLogics
{
    interface IFindCasesByTitle
    {
        IEnumerable<SI.Biz.Core.Bot.BotCase> GetCasesByTitle(string title);
    }

    sealed class FindCasesByTitle : IFindCasesByTitle
    {
        public IEnumerable<BotCase> GetCasesByTitle(string title)
        {
            throw new NotImplementedException();
        }
    }
}
