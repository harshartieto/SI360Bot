using SI.Biz.Core.Fluent;
using SI.Linq.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SI.Biz.Core.Bot.Case.RepositoryLogics
{
    interface IFindCasesByStatus
    {
        IEnumerable<SI.Biz.Core.Bot.BotCase> GetCasesByStatus(int caseStatusKey);
    }

    sealed class FindCasesByStatus : IFindCasesByStatus
    {
        public IEnumerable<BotCase> GetCasesByStatus(int caseStatusKey)
        {
            EnumCodeTableCasestatus enumCaseStatus;
            Enum.TryParse<EnumCodeTableCasestatus>(caseStatusKey.ToString(), out enumCaseStatus);
            var caseList = Get.Context.Case.Where(ca => ca.ToCaseStatusKey == enumCaseStatus
                           && ca.OurRef.OprJoin(JoinType.EqualJoin)
                           && ca.ToOrgUnit.OprJoin(JoinType.EqualJoin)
                           && ca.ToScrapCode.OprJoin(JoinType.EqualJoin))
                         .Select(ca => new
                         {
                             Recno = ca.Recno,
                             Title = ca.UnofficialTitle,
                             Description = ca.Description,
                             Notes = ca.Notes,
                             OurRefRecno = ca.OurRefKey,
                             OurRefSearchName = ca.OurRef.SearchName,
                             OurRefEmail = ca.OurRef.Email,
                             OrgUnitRecno = ca.ToOrgUnitKey,
                             OrgUnitSearchName = ca.ToOrgUnit.SearchName,
                             OrgUnitEmail = ca.ToOrgUnit.Email,
                             ScrapCode = ca.ToScrapCode.Code,
                             PreserveYears = ca.PreserveYears
                         });

            var botCases = new List<BotCase>();
            caseList.ForEach(caseInfo =>
            {
                botCases.Add(caseInfo == null
                         ? Empty<BotCase>.Default()
                         : new BotCase
                         {
                             Recno = caseInfo.Recno
                             ,
                             Title = caseInfo.Title
                             ,
                             Description = caseInfo.Description
                             ,
                             Notes = caseInfo.Notes
                             ,
                             OurRef = new BotContact
                             {
                                 Recno = caseInfo.OurRefRecno.HasValue ? caseInfo.OurRefRecno.Value : -1
                                 ,
                                 SearchName = caseInfo.OurRefSearchName
                                 ,
                                 Email = caseInfo.OurRefSearchName
                             }
                             ,
                             OrgUnit = new BotContact
                             {
                                 Recno = caseInfo.OrgUnitRecno.HasValue ? caseInfo.OurRefRecno.Value : -1
                                           ,
                                 SearchName = caseInfo.OrgUnitSearchName
                                           ,
                                 Email = caseInfo.OrgUnitEmail
                             }
                         });
            });


            return botCases;
        }
    }
}
