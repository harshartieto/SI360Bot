using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SI.Linq.Meta;
using SI.Biz.Core.Fluent;


namespace SI.Biz.Core.Bot.Case.RepositoryLogics
{
    interface IFindCaseByNumber
    {
        BotCase GetCaseByNumber(string caseNumber);
    }

    sealed class FindCaseByNumber : IFindCaseByNumber 
    {
        public BotCase GetCaseByNumber(string caseNumber)
        {

            var caseInfo = Get.Context.Case.Where(x => x.Name == caseNumber).OprFirstOrDefault();
            return caseInfo == null ? Empty<BotCase>.Default() : GetCaseInfoObject(caseInfo);
        }

        private static BotCase GetCaseInfoObject(Linq.Meta.Case caseInfo)
        {
            var caseDetails = new BotCase
            {
                Description = caseInfo.Description,
                External = caseInfo.ExternalKey,
                Notes = caseInfo.Notes,
                Recno = caseInfo.Recno,
                OurRef = caseInfo.OurRefKey,
                Title = caseInfo.UnofficialTitle
            };
            return caseDetails;
        }
    }
}
