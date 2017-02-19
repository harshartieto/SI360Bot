using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SI.Biz.Core.Bot.Case.RepositoryLogics
{
    interface IFindCasesByResponsible
    {
        IEnumerable<SI.Biz.Core.Bot.BotCase> GetCasesByResponsible(string responsible);
    }

    sealed class FindCasesByResponsible : IFindCasesByResponsible
    {
        public IEnumerable<BotCase> GetCasesByResponsible(string responsible)
        {
            throw new NotImplementedException();
        }
    }
}
