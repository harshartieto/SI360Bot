using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SI.Biz.Core.Bot.Case.RepositoryLogics
{
    interface IFindCasesByStatus
    {
        IEnumerable<SI.Biz.Core.Bot.BotCase> GetCasesByStatus(string caseStatus);
    }

    sealed class FindCasesByStatus : IFindCasesByStatus
    {
        public IEnumerable<BotCase> GetCasesByStatus(string caseStatus)
        {
            throw new NotImplementedException();
        }
    }
}
