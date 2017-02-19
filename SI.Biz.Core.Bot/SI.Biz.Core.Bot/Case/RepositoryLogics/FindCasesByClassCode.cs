using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SI.Biz.Core.Bot.Case.RepositoryLogics
{
    interface IFindCasesByClassCode
    {
        IEnumerable<SI.Biz.Core.Bot.BotCase> GetCasesByClassCode(string classCode);
    }

    sealed class FindCasesByClassCode : IFindCasesByClassCode
    {
        public IEnumerable<BotCase> GetCasesByClassCode(string classCode)
        {
            throw new NotImplementedException();
        }

    }
}
