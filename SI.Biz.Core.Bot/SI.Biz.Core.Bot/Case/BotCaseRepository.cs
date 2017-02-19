
using SI.Biz.Core.Bot.Case.RepositoryLogics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SI.Biz.Core.Bot.Case
{

    interface IBotCaseRepository
    {
        IEnumerable<BotCase> GetMyOpenCases(string userId, IFindMyOpenCases objGetMyOpenCases);

        IEnumerable<BotCase> FindMyCasesFromLastWeek(string userId, IFindCasesFromLastWeek objCaseFromLastWeek);

        BotCase FindCaseByNumber(string caseNumber, IFindCaseByNumber objCaseByNumber);

        IEnumerable<BotCase> FindCasesByTitle(string title, IFindCasesByTitle objCaseByTitle);

        IEnumerable<BotCase> FindCasesByResponsible(string responsiblePerson, IFindCasesByResponsible objCaseByResponsible);

        IEnumerable<BotCase> FindCasesByStatus(string status, IFindCasesByStatus objCaseByStatus);

        IEnumerable<BotCase> FindCasesByClassCode(string classCode, IFindCasesByClassCode objCaseByClassCode);

    }

    class BotCaseRepository : IBotCaseRepository
    {
        public IEnumerable<BotCase> GetMyOpenCases(string userId, IFindMyOpenCases objGetMyOpenCases)
        {
            return objGetMyOpenCases.GetMyOpenCases(userId);
        }

        public IEnumerable<BotCase> FindMyCasesFromLastWeek(string userId, IFindCasesFromLastWeek objCaseFromLastWeek)
        {
            return objCaseFromLastWeek.GetMyCasesFromLastWeek(userId);
        }

        public BotCase FindCaseByNumber(string caseNumber, IFindCaseByNumber objCaseByNumber)
        {
            return objCaseByNumber.GetCaseByNumber(caseNumber);
        }

        public IEnumerable<BotCase> FindCasesByTitle(string title, IFindCasesByTitle objCaseByTitle)
        {
            return objCaseByTitle.GetCasesByTitle(title);
        }

        public IEnumerable<BotCase> FindCasesByResponsible(string responsiblePerson, IFindCasesByResponsible objCaseByResponsible)
        {
            return objCaseByResponsible.GetCasesByResponsible(responsiblePerson);
        }

        public IEnumerable<BotCase> FindCasesByStatus(string status, IFindCasesByStatus objCaseByStatus)
        {
            return objCaseByStatus.GetCasesByStatus(status);
        }

        public IEnumerable<BotCase> FindCasesByClassCode(string classCode, IFindCasesByClassCode objCaseByClassCode)
        {
            return objCaseByClassCode.GetCasesByClassCode(classCode);
        }
    }
}
