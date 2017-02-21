using SI.Biz.Core.Fluent;
using SI.Linq.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SI.Biz.Core.Bot.Case.RepositoryLogics
{
    interface IFindAllCaseStatus
    {
        IEnumerable<SI.Biz.Core.Bot.BotCaseStatus> GetAllCaseStatus(string title);
    }

    sealed class FindAllCaseStatus : IFindAllCaseStatus
    {
        public IEnumerable<BotCaseStatus> GetAllCaseStatus(string title)
        {
            var caseStatus = Get.ContextACLBypassed.CodeTableCasestatus.Select(cs => cs);
            
            var botCaseStatusList = new List<BotCaseStatus>();
            caseStatus.ForEach(cs => { botCaseStatusList.Add(new BotCaseStatus { Recno = cs.Recno, Code = cs.Code, Description = cs.Description }); });
            return botCaseStatusList;
        }
    }
}
