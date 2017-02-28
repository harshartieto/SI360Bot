using SI.Biz.Core.Fluent;
using SI.Linq.Meta;
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
            if (string.IsNullOrEmpty(userId)) return Empty<BotCase>.DefaultList();

            var caseStatus = Get.ContextACLBypassed.CodeTableCasestatus.Where(cs => cs.Closed == 0 && cs.Voided == 0).Select(cs => cs.Recno).ToArray();

            //EnumCodeTableCasestatus enumCaseStatus;
            //Enum.TryParse<EnumCodeTableCasestatus>("B_5", out enumCaseStatus);
            var caa = Get.Context.Case.Where(f => f.ToCaseStatusKey == EnumCodeTableCasestatus.B_5).Select(x => new { x.Recno, x.ToCaseStatusKey });
            var caseList = Get.Context.Case.Where(ca => ca.ToCaseStatusKey.OprIn(caseStatus)
                           && ca.OurRef.OprJoin(JoinType.EqualJoin) 
                           //&& ca.OurRef.User.OprJoin(JoinType.EqualJoin)
                           //&& ca.OurRef.UserKey == Convert.ToInt32(userId)
                           && ca.ToOrgUnit.OprJoin(JoinType.LeftJoin)
                           && ca.ToScrapCode.OprJoin(JoinType.LeftJoin))
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
                                 Recno = caseInfo.OrgUnitRecno.HasValue ? caseInfo.OrgUnitRecno.Value : -1
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
